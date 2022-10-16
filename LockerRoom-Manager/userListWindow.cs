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
    public partial class userListWindow : Form
    {
        LoginUser selectedUser;
        public userListWindow()
        {
            InitializeComponent();
            DatabaseManager.RefreshUsersData();
            refreshListBox();
        }




        public void refreshListBox()
        {
            DatabaseManager.RefreshUsersData();
            ListBox1.Items.Clear();
            foreach (var item in DatabaseManager.LoginList)
            {
                ListBox1.Items.Add(item.Name);
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedUser = DatabaseManager.findUser(ListBox1.SelectedItem.ToString());
                textBox1.ReadOnly = selectedUser.Name == "admin";
                textBox1.Text = selectedUser.Name;
                textBox2.Text = selectedUser.Password;
            }
            catch
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = selectedUser.Name;
            textBox2.Text = selectedUser.Password;
        }
        private void Continue_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length >= 5 && textBox2.Text.Length >= 5)
            {
                label2.Hide();
                if ( textBox1.Text == selectedUser.Name || DatabaseManager.findUser(textBox1.Text) == null)
                {
                    label3.Hide();
                    selectedUser.Name = textBox1.Text;
                    selectedUser.Password = textBox2.Text;
                    DatabaseManager.updateUser(selectedUser);
                    refreshListBox();
                }
                else
                {
                    label3.Show();
                }
            }
            else
            {
                label2.Show();
            }

        }

        private void newUser_Click(object sender, EventArgs e)
        {
            Form newUserForm = new newUserWindow();
            newUserForm.Owner = this;
            newUserForm.Show();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex != -1)
            {
                if (selectedUser.Name != "admin")
                {
                    label4.Hide();
                    DatabaseManager.DeleteUser(selectedUser.ID.ToString());
                    refreshListBox();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    DatabaseManager.RefreshUsersData();
                }
                else
                {
                    label4.Show();
                }
            }
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Hide();
        }


    }
}
