using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yachne.Application.Account.Dtos
{
    public class LoginInput
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CaptchaId { get; set; }

        [Required]
        public string Captcha { get; set; }
    }
}
