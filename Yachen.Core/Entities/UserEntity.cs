using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Yachne.Authentication.Model;

namespace Yachne.Core.Entities
{
    [Table("T_User")]
    public class UserEntity : EntityBase, IUser
    {
        [Required]
        [Column("FUserName")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [Column("FPassword")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [Column("FSalt")]
        [StringLength(50)]
        public string Salt { get; set; }

    }
}
