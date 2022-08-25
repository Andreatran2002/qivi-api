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

        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<IBillRepository> _logger;
        public UserMutation(UserManager<ApplicationUser> userManager, ILogger<IBillRepository> logger)
		{
            _logger = logger; 
            _userManager = userManager; 
		}

        public async Task<AppResponse<User>> CreateUserAsync( string fullName , string phoneNumber, string address, string password,
            [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender ,[Service] UserManager<ApplicationUser> userManager)
        {
            try
            {
                var account = await userRepository.GetUserByPhoneNumber( phoneNumber);

                if (account == null)
                {
                    User? result;
                    String firstName = fullName.Split(' ')[0].ToLower();
                    String lastName = fullName.Split(' ')[fullName.Split(' ').Length-1].ToLower();

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
                        User newUser = new User(possibleUsername, fullName, phoneNumber, address);
                        result = await userRepository.InsertAsync(newUser);
                        await eventSender.SendAsync(nameof(Subscriptions.CustomerSubscription.OnCreateCustomer), result);
                        return new AppResponse<User>(result);
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
                        return new AppResponse<User>("undefined-error"+errors);
                    }
                }
                else
                {
                    return  new AppResponse<User>("account-available");

                }
            }
            catch(Exception e)
            {

                return new AppResponse<User>(e.Message);
            }
            
           
        }


        public async Task<User?> AuthenticationUserAsync(string name, string password, 
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
        public async Task<AppResponse<User>> AuthenticationByPhoneNumberAsync(string phoneNumber, string password,
           [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender, [Service] SignInManager<ApplicationUser> signInManager, [Service] UserManager<ApplicationUser> userManager)
        {
            try
            {
                var userByPhone = await userRepository.GetUserByPhoneNumber(phoneNumber);
                if (userByPhone == null)
                {
                    return new AppResponse<User>("account-not-available");
                }
                var appUser = await userManager.FindByNameAsync(userByPhone.UserName);
                SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Login account {phoneNumber}  successful");
                    return new AppResponse<User>(userByPhone);
                
                }
                else
                {
                    _logger.LogInformation($"Login account {phoneNumber} failure");

                    return new AppResponse<User>("wrong-password");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Authentication User Fail. ");
                _logger.LogError(e.ToString());
                return new AppResponse<User>("undefined-error");
            }


        }


        public User UpdateUser(User user,  [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        {
            var result =  userRepository.Update(user);

            if (result != null)
            {
                //await eventSender.SendAsync(nameof(Subscriptions.ProductSubscriptions.OnRemoveAsync), id);
            }

            return result;
        }
        public async Task<bool> RemoveUserAsync(string id, [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        {
            var result = await userRepository.RemoveAsync(id);

            if (result)
            {
                //await eventSender.SendAsync(nameof(Subscriptions.ProductSubscriptions.OnRemoveAsync), id);
            }

            return result;
        }
        public async Task<User> GetUserByIdAsync(string id, [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        =>  await userRepository.GetByIdAsync(id);

        public async Task<User> GetUserByPhoneAsync(string phoneNumber, [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        => await userRepository.GetUserByPhoneNumber(phoneNumber);

    }
}

