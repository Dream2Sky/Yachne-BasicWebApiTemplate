﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Yachne.Application.Account.Dtos
{
    public class LoginOutput
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
