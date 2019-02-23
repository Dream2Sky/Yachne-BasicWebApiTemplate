using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yachne.WebApi
{
    public class AuthManager
    {
        public AuthManager()
        {
            this.TokenDict = new Dictionary<int, string>();
        }
        public Dictionary<int, string> TokenDict { get; set; }
    }
}
