using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRoom_Manager
{
    internal static class dataManager
    {      
        //locker system

        public static List<LockerSheet> LockerSheets = new List<LockerSheet>();
        public static int currentSheetIndex = 0;

        public static LockerSheet currentSheet { get => LockerSheets[currentSheetIndex]; set => LockerSheets[currentSheetIndex] = value; }

        public static void DeleteLocker(int id)
        {
            try { LockerSheets[currentSheetIndex].lockers.Remove(LockerSheets[currentSheetIndex].lockers.Find(x => x.ID == id)); } catch { }
        }
        public static Locker CreateLocker(int[] coords)
        {
            Locker newLocker = new Locker(LockerSheets[dataManager.currentSheetIndex].lockers.Count != 0 ? LockerSheets[currentSheetIndex].lockers.Max(x => x.ID) + 1 : 1, coords);
            LockerSheets[dataManager.currentSheetIndex].lockers.Add(newLocker);
            return newLocker;
        }
        public static void CreateLocker(Locker newLocker)
        {
            LockerSheets[dataManager.currentSheetIndex].lockers.Add(newLocker);
        }
        public static void UpdateLocker(Locker newLocker)
        {
            LockerSheets[dataManager.currentSheetIndex].lockers[LockerSheets[dataManager.currentSheetIndex].lockers.IndexOf(FindLocker(newLocker.ID))].Coords = newLocker.Coords;
        }
        public static Locker FindLocker(int id)
        {
            foreach (Locker locker in LockerSheets[dataManager.currentSheetIndex].lockers)
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
