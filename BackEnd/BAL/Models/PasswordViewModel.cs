using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class PasswordViewModel
    {
        [Required]
        public string Otp { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public string ComrfirmPassword { get; set; }
    }
}
