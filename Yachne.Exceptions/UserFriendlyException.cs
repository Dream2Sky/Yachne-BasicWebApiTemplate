using System;

namespace Yachne.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
