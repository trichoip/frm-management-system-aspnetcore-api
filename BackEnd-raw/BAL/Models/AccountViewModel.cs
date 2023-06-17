using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class AccountViewModel
    {
        public string email { get; set; }
        public string password { get; set; }

       
        /*public AccountViewModel(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public AccountViewModel(string password)
        {
            this.password = password;
        }*/
    }

}
