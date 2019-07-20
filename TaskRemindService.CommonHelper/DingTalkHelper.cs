using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TipService.CommonHelper
{
    public static class DingTalkHelper
    {
        public static string GetAccessToken()
        {
            string apiUrl = "https://oapi.dingtalk.com/gettoken?corpid=ding08a708c5272bc85d35c2f4657eb6378f&corpsecret=o4Ivoh4T7MfhOGf2wlIZmzrUih03dDw2OcvekuZOGUohFj-CvlyOej2DZHRx_-By";
            string result = WebServiceHelper.HttpGet(apiUrl, null);
            return result;
        }
    }
}
