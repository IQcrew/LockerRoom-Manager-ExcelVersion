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
        static IFirebaseClient client;
        static IFirebaseConfig database = new FirebaseConfig()             // connect to your Firebase database
        {
            AuthSecret = "kCO10F4Dd7pkPJiPoicxfHSy1kGoxGsyHxdoFATF",
            BasePath = "https://skrinky-30a5a-default-rtdb.firebaseio.com"

        };
        public static void SetUp()
        {
            try
            {
                client = new FireSharp.FirebaseClient(database);
            }
            catch
            {
                MessageBox.Show("Out of internet");
            }
            RefreshLockersData();
        }
        public static string getBackup()
        {
            FirebaseResponse res = client.Get("Lockers");
            return res.Body.ToString();
        }
        public static void setBackup(string backup)
        {
            client.Delete("Lockers");
            Dictionary<string, Locker> data = JsonConvert.DeserializeObject<Dictionary<string, Locker>>(backup);
            client.Set("Lockers", data);
            RefreshLockersData();
        }



      
        //locker system

        public static List<Locker> LockersList = new List<Locker>();
        public static List<LockerSheet> LockerSheets = new List<LockerSheet>();
        public static void RefreshLockersData()
        {
            FirebaseResponse res = client.Get("Lockers");
            Dictionary<string, Locker> data = JsonConvert.DeserializeObject<Dictionary<string, Locker>>(res.Body.ToString());
            LockersList.Clear();
            if (data != null)
            {
                foreach (string key in data.Keys)
                {
                    data[key].ID -= 1000;
                    LockersList.Add(data[key]);
                }
            }
        }
        public static void newRefreshLockersData()
        {


        }
        public static void DeleteLocker(int id)
        {
            client.Delete("Lockers/" + (id + 1000).ToString());
            RefreshLockersData();
        }
        public static Locker CreateLocker(int[] coords)
        {
            Locker newLocker = new Locker(LockersList.Count != 0 ? LockersList[LockersList.Count - 1].ID + 1000 + maxNum() : 1001, coords);
            client.Set("Lockers/" + newLocker.ID, newLocker);
            newLocker.ID -= 1000;
            LockersList.Add(newLocker);
            return newLocker;

        }
        public static void CreateLocker(Locker newLocker)
        {
            newLocker.ID += 1000;
            client.Set("Lockers/" + newLocker.ID, newLocker);
            newLocker.ID -= 1000;
            LockersList.Add(newLocker);
        }
        public static void UpdateLocker(Locker newLocker)
        {
            newLocker.ID += 1000;
            client.Update("Lockers/" + newLocker.ID, newLocker);
            newLocker.ID -= 1000;
            LockersList[LockersList.IndexOf(FindLocker(newLocker.ID))].Coords = newLocker.Coords;
        }
        public static Locker FindLocker(int id)
        {
            foreach (Locker locker in LockersList)
            {
                if (locker.ID == id)
                {
                    return locker;
                }
            }
            return null;
        }
        private static int maxNum()
        {
            for (int i = 0; i < LockersList.Count; i++)
            {
                int tempNum = 0;
                if (LockersList[i].ID > tempNum) { tempNum = LockersList[i].ID;}
            }
            return 1;
        }
    }
}
