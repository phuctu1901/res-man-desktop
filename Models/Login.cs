using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Models
{
    class Login
    {
        
        public string email { get; set; }
        public string password { get; set; }
        public bool remember_me { get; set; }
        
        public Login(string email, string password, bool remember_me)
        {
            this.email = email;
            this.password = password;
            this.remember_me = remember_me;
        }
    }


}
