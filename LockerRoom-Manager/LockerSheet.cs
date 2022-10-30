using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerRoom_Manager
{
    internal class LockerSheet
    {
        public string Name;
        public List<Locker> lockers = new List<Locker>();
        
        public LockerSheet(string name)
        {
            Name = name;
        }
    }
}
