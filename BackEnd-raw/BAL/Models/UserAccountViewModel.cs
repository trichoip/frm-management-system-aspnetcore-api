using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class UserAccountViewModel
    {
        public string UserName { get; set; }
        public string Password{ get; set; }
        public string Fullname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public long IdRole { get; set; }
    }
}
