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
    public partial class newUserWindow : Form
    {
        public newUserWindow()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 5 && textBox2.Text.Length >= 5)
            {
                label2.Hide();
                if (DatabaseManager.findUser(textBox1.Text) == null)
                {
                    label3.Hide();
                    DatabaseManager.CreateUser(textBox1.Text,textBox2.Text);
                    (this.Owner as userListWindow).refreshListBox();
                    this.Close();
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
        private void Cancel_Click(object sender, EventArgs e)
        {
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
        }
    }
}
