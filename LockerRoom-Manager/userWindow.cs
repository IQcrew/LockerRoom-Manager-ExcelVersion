using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRoom_Manager
{
    public partial class userWindow : Form
    {
        Point CursorMouseCoords = new Point();
        Point LastObjectCoords = new Point();
        PictureBox selectedLocker = new PictureBox();
        Label selectedLabel = new Label();
        public List<int> openLockerTabs = new List<int>();

        public userWindow()
        {
            InitializeComponent();
            DatabaseManager.RefreshLockersData();
            foreach (Locker lckr in DatabaseManager.LockersList) { this.printNewLocker(lckr.ID, lckr.Coords, lckr.NameOfHolder == "" && lckr.HolderClass == ""); }
            nameBox_TextChanged(null, null);
        }
        private void locker_MouseDown(object sender, MouseEventArgs e)
        {
            selectedLocker = sender as PictureBox;
            selectedLabel = LockersNumbers[selectedLocker];
        }
        private void locker_MouseDownFromLabel(object sender, MouseEventArgs e)
        {
            selectedLabel = sender as Label;
            selectedLocker = this.getLockerPictureBox(Int32.Parse(selectedLabel.Text));

        }
        private void openLockerProperties(object sender, MouseEventArgs e)
        {
            if (openLockerTabs.Contains(Int32.Parse(selectedLabel.Text))) { return; }
            try
            {
                Form LockerPropertiesForm = new LockerPropertiesWindow(Int32.Parse(selectedLabel.Text));
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
                Form LockerPropertiesForm = new LockerPropertiesWindow(temp);
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
                if (item.NameOfHolder.ToLower().Contains(nameBox.Text.ToLower())) { listBox1.Items.Add(item.ID.ToString() + " - " + item.NameOfHolder); }
            }
        }

        // WINDOW DESINGN
        bool MouseHold = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MouseHold) { this.Location = new Point(LastObjectCoords.X - (CursorMouseCoords.X - Cursor.Position.X), LastObjectCoords.Y - (CursorMouseCoords.Y - Cursor.Position.Y)); }
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
