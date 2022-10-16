using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;

namespace LockerRoom_Manager
{
    public partial class adminWindow : Form
    {
        bool MouseHoldLocker = false;
        Point CursorMouseCoords = new Point();
        Point LastObjectCoords = new Point();
        PictureBox selectedLocker = new PictureBox();
        Label selectedLabel = new Label();

        public List<int> openLockerTabs = new List<int>();

        Form userListForm = new userListWindow();
        public adminWindow()
        {
            InitializeComponent();
            userListForm.Owner = this;
            DatabaseManager.RefreshLockersData();
            foreach  (Locker lckr in DatabaseManager.LockersList){this.printNewLocker(lckr.ID,lckr.Coords, lckr.NameOfHolder =="" && lckr.HolderClass == ""); }
            nameBox_TextChanged(null, null);
            saveFileDialog1.Filter = "Data Files (*.lckm)|*.lckm";
            saveFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "Data Files (*.lckm)|*.lckm";
            openFileDialog1.AddExtension = true;
        }

        private void userList_Click(object sender, EventArgs e)
        {
            userListForm.Show();
            this.Hide();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createNewLocker();
        }
        private void locker_MouseDown(object sender, MouseEventArgs e)
        {

            if(sender.GetType().ToString() == "System.Windows.Forms.PictureBox")
            {
                selectedLocker = sender as PictureBox;
                selectedLabel = LockersNumbers[selectedLocker];
            }
            else
            {
                selectedLabel = sender as Label;
                selectedLocker = this.getLockerPictureBox(Int32.Parse(selectedLabel.Text));
            }
            selectedLocker.BringToFront();
            selectedLabel.BringToFront();
            CursorMouseCoords = Cursor.Position;
            LastObjectCoords = selectedLocker.Location;
            MouseHoldLocker = true;

        }
        private void locker_MouseUp()
        {
            selectedLocker.BorderStyle = BorderStyle.None;
            MouseHoldLocker = false;
            if(selectedLocker.Location == LastObjectCoords) { return; }
            if (possiblePos(selectedLocker.Location.X, selectedLocker.Location.Y))
            {
                Locker tempLocker = DatabaseManager.FindLocker(Int32.Parse(selectedLabel.Text));
                tempLocker.Coords = new int[] { selectedLocker.Location.X, selectedLocker.Location.Y };
                DatabaseManager.UpdateLocker(tempLocker);
            }
            else
            {
                selectedLocker.Location = LastObjectCoords;
                selectedLabel.Location = new Point(selectedLocker.Location.X + 8, selectedLocker.Location.Y + 60);
            }
            selectedLocker.BorderStyle = BorderStyle.None;
        }
        private void locker_LocationChanged(object sender, EventArgs e)
        {
            selectedLocker.BorderStyle = BorderStyle.Fixed3D;
        }
        private Point getCoordsOfLocker()  // coords limits 
        {
            int x = ((LastObjectCoords.X - (CursorMouseCoords.X - Cursor.Position.X))/10)*10;
            int y = ((LastObjectCoords.Y - (CursorMouseCoords.Y - Cursor.Position.Y))/10)*10;
            if (x < 0) { x = 0; }
            if (y < 0) { y = 0; }

            return new Point(x,y);
        }

        private bool possiblePos(int x, int y)
        {
            
            foreach (Locker lckr in DatabaseManager.LockersList)
            {
                if ( selectedLabel.Text != lckr.ID.ToString() && Math.Abs(lckr.Coords[0] - x) <=39 && Math.Abs(lckr.Coords[1] - y) <= 89) { return false; }
            }

            return true;
        }

        private void createNewLocker()
        {
            if (possiblePos(0, 0))
            {
                Locker newLocker = DatabaseManager.CreateLocker(new int[] {0,0});
                this.printNewLocker(newLocker.ID,newLocker.Coords, true);
            }
        }


        private void openLockerProperties(object sender, MouseEventArgs e)
        {
            if (openLockerTabs.Contains(Int32.Parse(selectedLabel.Text))) { return; }
            try
            {
                Form LockerPropertiesForm = new LockerPropertiesAdminWindow(Int32.Parse(selectedLabel.Text));
                LockerPropertiesForm.Owner = this;
                LockerPropertiesForm.Show();
                LockerPropertiesForm.Location = Cursor.Position;
            }
            catch { }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int temp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
                if (openLockerTabs.Contains(temp)) { return; }
                Form LockerPropertiesForm = new LockerPropertiesAdminWindow(temp);
                LockerPropertiesForm.Owner = this;
                LockerPropertiesForm.Show();
                LockerPropertiesForm.Location = Cursor.Position;
            }
            catch { }
        }

        public void nameBox_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Locker item in DatabaseManager.LockersList)
            {
                if (item.NameOfHolder.ToLower().Contains(nameBox.Text.ToLower())){listBox1.Items.Add(item.ID.ToString() +" - "+item.NameOfHolder);}
            }
        }
        private void ImportBackup_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                string backup = File.OpenText(openFileDialog1.FileName).ReadToEnd();
                DatabaseManager.setBackup(backup);
                this.Controls.Clear();
                this.InitializeComponent();
                foreach (Locker lckr in DatabaseManager.LockersList) { this.printNewLocker(lckr.ID, lckr.Coords, lckr.NameOfHolder == "" && lckr.HolderClass == ""); }
                nameBox_TextChanged(null, null);
            }
            catch { }
        }

        private void ExportBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string backup = DatabaseManager.getBackup();
                saveFileDialog1.ShowDialog();
                File.WriteAllText(saveFileDialog1.FileName, backup);
            }
            catch { }
        }



        // WINDOW DESINGN
        bool MouseHold = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MouseHold) { this.Location = new Point(LastObjectCoords.X - (CursorMouseCoords.X - Cursor.Position.X), LastObjectCoords.Y - (CursorMouseCoords.Y - Cursor.Position.Y)); }
            else if(MouseHoldLocker) {
                if (Control.MouseButtons != MouseButtons.Left)
                {
                    locker_MouseUp();
                    return;
                }
                selectedLocker.Location = getCoordsOfLocker();
                selectedLabel.Location = new Point(selectedLocker.Location.X + 8, selectedLocker.Location.Y + 60);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            CursorMouseCoords = Cursor.Position;
            LastObjectCoords = Location;
            MouseHold = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseHold = false;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(255, 0, 44, 51);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Black;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(255, 0, 44, 51);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitProgram(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
        }
    }
}