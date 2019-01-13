using System;
using System.Collections.Generic;
using System.Text;

namespace Yachne.Authentication.Model
{
    public interface IUser
    {
        int Id { get; set; }
        string UserName { get; set; }
    }
}
