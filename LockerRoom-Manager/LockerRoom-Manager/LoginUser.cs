using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerRoom_Manager
{
    internal class LoginUser
    {
        public LoginUser(string Name, string Password, int ID)
        {
            this.Name = Name;
            this.Password = Password;
            this.ID = ID;

        }
        public string Name { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }
    }
}
