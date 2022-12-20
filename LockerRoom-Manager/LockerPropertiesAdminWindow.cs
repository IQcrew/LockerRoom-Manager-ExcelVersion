using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LockerRoom_Manager
{
    public partial class LockerPropertiesAdminWindow : Form
    {
        Locker lckr;
        adminWindow main;
        int originalID;
        public LockerPropertiesAdminWindow(int id)
        {
            InitializeComponent();
            originalID = id;

            try
            {
                string[] classList = File.ReadAllLines(".\\zoznamTried.txt");
                foreach (var item in classList)
                {
                    if (item.Trim() != "") { classBox.Items.Add(item.Trim()); }
                }
            }
            catch { }
        }
        private void LockerPropertiesAdminWindow_Load(object sender, EventArgs e)
        {
            main = this.Owner as adminWindow;
            main.openLockerTabs.Add(originalID);
            lckr = dataManager.FindLocker(originalID);
            idBox.Text = lckr.ID.ToString();
            nameBox.Text = lckr.NameOfHolder;
            classBox.Text = lckr.HolderClass;
        }
        private void saveChanges_Click(object sender, EventArgs e)
        {
            label3.Hide();
            if (idBox.Text == lckr.ID.ToString()){
                main.LockerState(lckr.ID, nameBox.Text == "" && classBox.Text == "");
                lckr.NameOfHolder = nameBox.Text;
                lckr.HolderClass = classBox.Text;
                dataManager.UpdateLocker(lckr);
                main.openLockerTabs.Remove(originalID);
                main.filter_TextChanged(null, null);
                this.Close();
            }
            else
            {
                if (dataManager.FindLocker(Int32.Parse(idBox.Text)) == null)
                {
                    main.clearSelectedLockers();
                    main.openLockerTabs.Remove(originalID);
                    main.filter_TextChanged(null, null);
                    dataManager.DeleteLocker(lckr.ID);
                    main.earseLocker(lckr.ID);
                    lckr.NameOfHolder = nameBox.Text;
                    lckr.HolderClass = classBox.Text;
                    lckr.ID = Int32.Parse(idBox.Text);
                    main.printNewLocker(lckr.ID, lckr.Coords, nameBox.Text == "" && classBox.Text == "");
                    dataManager.CreateLocker(lckr);
                    main.LockerState(lckr.ID, nameBox.Text == "" && classBox.Text == "");
                    this.Close();
                }
                else
                {
                    label3.Show();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            main.earseLocker(lckr.ID);
            dataManager.DeleteLocker(lckr.ID);
            main.openLockerTabs.Remove(originalID);
            this.Close();
        }


        // WINDOW DESINGN
        bool MouseHold = false;
        Point CursorMouseCoords = new Point();
        Point LastObjectCoords = new Point();
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
            pictureBox3.BackColor = Color.FromArgb(255, 0, 49, 62);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Black;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(255, 0, 49, 62);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Owner.Show();
            main.openLockerTabs.Remove(originalID);
            this.Hide();
        }

        private void idBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
        }
    }
}
