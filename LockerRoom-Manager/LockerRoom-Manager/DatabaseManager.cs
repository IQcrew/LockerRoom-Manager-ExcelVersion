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
    internal static class DatabaseManager
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
            RefreshUsersData();
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



        //login system


        public static List<LoginUser> LoginList = new List<LoginUser>();
        public static void RefreshUsersData()
        {
            try
            {
                FirebaseResponse res = client.Get("Users");
                Dictionary<string, LoginUser> data = JsonConvert.DeserializeObject<Dictionary<string, LoginUser>>(res.Body.ToString());
                LoginList.Clear();
                if (data != null)
                {
                    foreach (string key in data.Keys)
                    {
                        LoginList.Add(data[key]);
                    }
                }
            }
            catch {
                MessageBox.Show("Out of internet");
            }
        }
        public static void DeleteUser(string id)
        {
            client.Delete("Users/" + id);
            RefreshUsersData();
        }
        public static void CreateUser(string Name, string Password)
        {
            LoginUser newUser = new LoginUser(Name, Password, LoginList[LoginList.Count - 1].ID + 1);
            LoginList.Add(newUser);
            client.Set("Users/" + newUser.ID, newUser);

        }
        public static void updateUser(LoginUser newUser)
        {
            client.Update("Users/" + newUser.ID, newUser);
        }
        public static LoginUser findUser(string name)
        {
            foreach (LoginUser user in LoginList)
            {
                if (user.Name == name) { return user; }
            }
            return null;
        }




        //locker system

        public static List<Locker> LockersList = new List<Locker>();
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
