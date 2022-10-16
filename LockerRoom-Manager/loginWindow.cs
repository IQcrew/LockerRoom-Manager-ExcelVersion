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
    public partial class loginWindow : Form
    {

        public loginWindow()
        {
            InitializeComponent();
            DatabaseManager.SetUp();
        }


        private void Continue_Click(object sender, EventArgs e)
        {
            foreach (var user in DatabaseManager.LoginList)
            {
                if (textBox1.Text == user.Name && textBox2.Text == user.Password)
                {
                    if(textBox1.Text == "admin")
                    {
                        Form adminForm = new adminWindow();
                        adminForm.Show();
                        this.Hide();
                        return;
                    }
                    else
                    {
                        Form userForm = new userWindow();
                        userForm.Show();
                        this.Hide();
                        return;
                    }
                   
                }
            }
            label2.Visible = true;
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
            Application.Exit();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                Continue_Click(null, null);
            }
        }
    }
}
