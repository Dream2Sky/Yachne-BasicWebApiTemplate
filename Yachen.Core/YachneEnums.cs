using System;

namespace Yachne.Core
{
    public class YachneEnums
    {
        public enum WebApiStatusCode
        {
            Success = 10000,
            GeneralError = 10001,
            NotFound = 10002,
            InValidInput = 10003,

            LoginFaild = 20001,
            InValidLoginStatus = 20002,
            CaptchaExpired = 20003,
            InValidCaptcha = 20004,

            TimeOut = 40001,
        }
    }

}
