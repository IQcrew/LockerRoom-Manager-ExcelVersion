using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerRoom_Manager
{
    internal class LockerSheet
    {
        public string Name { get; set; }
        public List<Locker> lockers { get; set; }
        public LockerSheet(string name)
        {
            Name = name;
        }
    }
}
