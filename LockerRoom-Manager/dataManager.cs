using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace LockerRoom_Manager
{
    internal static class dataManager
    {      
        //locker system

        public static List<LockerSheet> LockerSheets = new List<LockerSheet>();
        public static int currentSheet = 0;

        public static void DeleteLocker(int id)
        {
            try { LockerSheets[currentSheet].lockers.Remove(LockerSheets[currentSheet].lockers.Find(x => x.ID == id)); } catch { }
        }
        public static Locker CreateLocker(int[] coords)
        {
            Locker newLocker = new Locker(LockerSheets[dataManager.currentSheet].lockers.Count != 0 ? LockerSheets[currentSheet].lockers.Max(x => x.ID) + 1 : 1, coords);
            LockerSheets[dataManager.currentSheet].lockers.Add(newLocker);
            return newLocker;
        }
        public static void CreateLocker(Locker newLocker)
        {
            LockerSheets[dataManager.currentSheet].lockers.Add(newLocker);
        }
        public static void UpdateLocker(Locker newLocker)
        {
            LockerSheets[dataManager.currentSheet].lockers[LockerSheets[dataManager.currentSheet].lockers.IndexOf(FindLocker(newLocker.ID))].Coords = newLocker.Coords;
        }
        public static Locker FindLocker(int id)
        {
            foreach (Locker locker in LockerSheets[dataManager.currentSheet].lockers)
            {
                if (locker.ID == id)
                {
                    return locker;
                }
            }
            return null;
        }

    }
}
