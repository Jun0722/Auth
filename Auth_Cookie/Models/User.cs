using System;
using System.ComponentModel.DataAnnotations;

namespace Auth_Cookie.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        public string Nothing { get; set; }
    }
}
