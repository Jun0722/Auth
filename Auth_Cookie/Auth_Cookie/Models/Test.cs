using System;
using System.ComponentModel.DataAnnotations;

namespace Auth_Cookie.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="某人")]
        public string Someone { get; set; }

        [Required]
        [Display(Name = "某事")]
        public string Something { get; set; }
    }
}
