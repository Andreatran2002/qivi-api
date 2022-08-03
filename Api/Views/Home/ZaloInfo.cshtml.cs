using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZaloDotNetSDK;

namespace Api.Views.Home
{
	public class ZaloInfoModel : PageModel
    {
        
        public void OnGet()
        {

            //ZaloAppInfo appInfo = new ZaloAppInfo(2088490447091653618, "S5Yq6BT2OVY1IDmRG8E8", "callbackUrl");
            //Za/loAppClient appClient = new ZaloAppClient(appInfo);
        }
    }
}
