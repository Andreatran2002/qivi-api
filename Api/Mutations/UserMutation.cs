using System;
using System.Net;
using Core.Base;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Identity;

namespace Api.Mutations
{
    [ExtendObjectType(nameof(Mutation))]

    public class UserMutation
	{

        private readonly ILogger<IUserRepository> _logger;
        public UserMutation( ILogger<IUserRepository> logger)
		{
            _logger = logger; 
		}

        public async Task<AppResponse<ApplicationUser>> CreateUserAsync( string fullName , string phoneNumber, string address, string password,
            [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender ,[Service] UserManager<ApplicationUser> userManager)
        {
            try
            {
                var account = await userRepository.GetUserByPhoneNumber( phoneNumber);

                if (account == null)
                {
                    string firstName = fullName.Split(' ')[0].ToLower();
                    string lastName = fullName.Split(' ')[fullName.Split(' ').Length-1].ToLower();

                    var possibleUsername = string.Format("{0}{1}", lastName, firstName);

                    var existingUsers =  userRepository.GetPossibleUserName(possibleUsername);

                   if (existingUsers.Count !=0)
                    {
                        possibleUsername += (existingUsers.Count).ToString();
                    }


                    ApplicationUser appUser = new ApplicationUser
                    {
                        UserName = possibleUsername,
                        PhoneNumber = phoneNumber,
                    };
                    IdentityResult identityResult = await userManager.CreateAsync(appUser, password);
                    if (identityResult.Succeeded)
                    {
                        _logger.LogInformation($"Create new user {possibleUsername}  {phoneNumber} successful");
                        return new AppResponse<ApplicationUser>(appUser);
                    }
                    else
                    {
                        _logger.LogInformation($"Create new user {possibleUsername}  {phoneNumber} failure");
                        String errors = "";

                        foreach (IdentityError error in identityResult.Errors)
                        {
                            errors += error.Description; 
                            _logger.LogError(error.Description);


                        }
                        return new AppResponse<ApplicationUser>("undefined-error"+errors);
                    }
                }
                else
                {
                    return  new AppResponse<ApplicationUser>("account-available");

                }
            }
            catch(Exception e)
            {

                return new AppResponse<ApplicationUser>(e.Message);
            }
            
           
        }


        public async Task<ApplicationUser?> AuthenticationUserAsync(string name, string password, 
           [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender,[Service] SignInManager<ApplicationUser> signInManager, [Service] UserManager<ApplicationUser> userManager)
        {
            try
            {
                var appUser = await userManager.FindByNameAsync(name);
                SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Login account {name}  successful");
                    return await userRepository.GetUserByName(name);
                }
                else
                {
                    _logger.LogInformation($"Login account {name} failure");

                    return null;
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Authentication User Fail. ");
                _logger.LogError(e.ToString());
                return null; 
            }
            

        }
        public async Task<AppResponse<ApplicationUser>> AuthenticationByPhoneNumberAsync(string phoneNumber, string password,
           [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender, [Service] SignInManager<ApplicationUser> signInManager, [Service] UserManager<ApplicationUser> userManager)
        {
            try
            {
                var userByPhone = await userRepository.GetUserByPhoneNumber(phoneNumber);
                if (userByPhone == null)
                {
                    return new AppResponse<ApplicationUser>("account-not-available");
                }
                var appUser = await userManager.FindByNameAsync(userByPhone.UserName);
                SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Login account {phoneNumber}  successful");
                    return new AppResponse<ApplicationUser>(userByPhone);
                
                }
                else
                {
                    _logger.LogInformation($"Login account {phoneNumber} failure");

                    return new AppResponse<ApplicationUser>("wrong-password");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Authentication User Fail. ");
                _logger.LogError(e.ToString());
                return new AppResponse<ApplicationUser>("undefined-error");
            }


        }


        public async Task<AppResponse<bool>> ChangePassword(string id , string password, [Service] UserManager<ApplicationUser> userManager,  [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return new AppResponse<bool>("user-not-available");
                }
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, password);
                if (result.Succeeded)
                {
                    return new AppResponse<bool>(true);
                }
                else return new AppResponse<bool>(false); 
            }catch(Exception e)
            {
                return new AppResponse<bool>("undefined-error");
            }
            
        }
        //public async Task<AppResponse<bool>> LockUser(string id, [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(id);
        //        if (user == null)
        //        {
        //            return new AppResponse<bool>("user-not-available");
        //        }
        //        _
        //        if (result.Succeeded)
        //        {
        //            return new AppResponse<bool>(true);
        //        }
        //        else return new AppResponse<bool>(false);
        //    }
        //    catch (Exception e)
        //    {
        //        return new AppResponse<bool>("undefined-error");
        //    }
        //}
       

    }
}

