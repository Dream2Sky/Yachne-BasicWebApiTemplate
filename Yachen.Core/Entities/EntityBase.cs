using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Yachne.Core.Entities
{
    public class EntityBase
    {
        [Required]
        [Column("FID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("FCreateTime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Required]
        [Column("FIsForbidden")]
        public bool IsForbidden { get; set; } = false;

        [Required]
        [Column("FForbiddenTime")]
        public DateTime ForbiddenTime { get; set; } = DateTime.Now;
    }
}
