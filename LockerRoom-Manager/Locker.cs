using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerRoom_Manager
{
    internal class Locker
    {
        public Locker(int id, int[] coords)
        {
            this.ID = id;
            this.Coords = coords;
        }



        public int ID;
        public string NameOfHolder = "";
        public string HolderClass = "";
        public int[] Coords = { 0, 0 }; // x, y
    }

}
