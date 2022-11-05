﻿using System;
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
using Microsoft.Vbe.Interop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Net.Http.Headers;
using Microsoft.Office.Interop.Excel;

namespace LockerRoom_Manager
{
    public partial class adminWindow : Form
    {
        
        bool MouseHoldLocker = false;
        System.Drawing.Point CursorMouseCoords = new System.Drawing.Point();
        System.Drawing.Point LastObjectCoords = new System.Drawing.Point();
        PictureBox selectedLocker = new PictureBox();
        System.Windows.Forms.Label selectedLabel = new System.Windows.Forms.Label();

        public List<int> openLockerTabs = new List<int>();


        public adminWindow()
        {
            InitializeComponent();
            nameBox_TextChanged(null, null);
            saveFileDialog1.Filter = "Data Files (*.xlsx)|*.xlsx";
            saveFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "Data Files (*.xlsx)|*.xlsx";
            openFileDialog1.AddExtension = true;
            
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            createNewLocker();
        }
        private void locker_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e)
        {

            if(sender.GetType().ToString() == "System.Windows.Forms.PictureBox")
            {
                selectedLocker = sender as PictureBox;
                selectedLabel = LockersNumbers[selectedLocker];
            }
            else
            {
                selectedLabel = sender as System.Windows.Forms.Label;
                selectedLocker = this.getLockerPictureBox(Int32.Parse(selectedLabel.Text));
            }
            selectedLocker.BringToFront();
            selectedLabel.BringToFront();
            CursorMouseCoords = System.Windows.Forms.Cursor.Position;
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
                Locker tempLocker = dataManager.FindLocker(Int32.Parse(selectedLabel.Text));
                tempLocker.Coords = new int[] { selectedLocker.Location.X, selectedLocker.Location.Y };
                dataManager.UpdateLocker(tempLocker);
            }
            else
            {
                selectedLocker.Location = LastObjectCoords;
                selectedLabel.Location = new System.Drawing.Point(selectedLocker.Location.X + 8, selectedLocker.Location.Y + 60);
            }
            selectedLocker.BorderStyle = BorderStyle.None;
        }
        private void locker_LocationChanged(object sender, EventArgs e)
        {
            selectedLocker.BorderStyle = BorderStyle.Fixed3D;
        }
        private System.Drawing.Point getCoordsOfLocker()  // coords limits 
        {
            int x = ((LastObjectCoords.X - (CursorMouseCoords.X - System.Windows.Forms.Cursor.Position.X))/10)*10;
            int y = ((LastObjectCoords.Y - (CursorMouseCoords.Y - System.Windows.Forms.Cursor.Position.Y))/10)*10;
            if (x < 0) { x = 0; }
            if (y < 0) { y = 0; }

            return new System.Drawing.Point(x,y);
        }

        private bool possiblePos(int x, int y)
        {
            foreach (Locker lckr in dataManager.LockerSheets[dataManager.currentSheet].lockers)
            {
                if ( selectedLabel.Text != lckr.ID.ToString() && Math.Abs(lckr.Coords[0] - x) <=39 && Math.Abs(lckr.Coords[1] - y) <= 89) { return false; }
            }

            return true;
        }

        private void createNewLocker()
        {
            if (dataManager.LockerSheets.Count < 1) { return; }
            if (possiblePos(0, 0))
            {
                Locker newLocker = dataManager.CreateLocker(new int[] {0,0});
                this.printNewLocker(newLocker.ID,newLocker.Coords, true);
            }
        }


        private void openLockerProperties(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (openLockerTabs.Contains(Int32.Parse(selectedLabel.Text))) { return; }
            try
            {
                Form LockerPropertiesForm = new LockerPropertiesAdminWindow(Int32.Parse(selectedLabel.Text));
                LockerPropertiesForm.Owner = this;
                LockerPropertiesForm.Show();
                LockerPropertiesForm.Location = System.Windows.Forms.Cursor.Position;
            }
            catch { }
        }


        private void listBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                int temp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
                if (openLockerTabs.Contains(temp)) { return; }
                Form LockerPropertiesForm = new LockerPropertiesAdminWindow(temp);
                LockerPropertiesForm.Owner = this;
                LockerPropertiesForm.Show();
                LockerPropertiesForm.Location = System.Windows.Forms.Cursor.Position;
            }
            catch { }
        }

        public void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (dataManager.LockerSheets.Count < 1) { return; }
            listBox1.Items.Clear();
            foreach (Locker item in dataManager.LockerSheets[dataManager.currentSheet].lockers)
            {
                if (item.NameOfHolder.ToLower().Contains(nameBox.Text.ToLower())){listBox1.Items.Add(item.ID.ToString() +" - "+item.NameOfHolder);}
            }
        }
        private void ImportBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if(openFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                label1.Text = openFileDialog1.SafeFileName;
                string backup = File.ReadAllText(openFileDialog1.FileName);
                dataManager.LockerSheets.Clear();
                classBox.Items.Clear();
                var package = new ExcelPackage(openFileDialog1.FileName);
                var wb = package.Workbook;
                for (int x = 0; x < wb.Worksheets.Count; x++)
                {
                    var ws = wb.Worksheets[x];
                    if (ws.Cells[1, 1].Value.ToString() != "Locker_Room_File") { continue; }
                    LockerSheet tempSheet = new LockerSheet(ws.Cells[2, 1].Value.ToString());
                    for (int i = 4; i <= ws.Dimension.Rows; i++)
                    {
                        Locker tempLocker = new Locker(Int32.Parse(ws.Cells[i, 1].Value.ToString()), Array.ConvertAll(ws.Cells[i, 4].Value.ToString().Split(','), Int32.Parse));
                        tempLocker.NameOfHolder = ws.Cells[i, 2].Value.ToString();
                        tempLocker.HolderClass = ws.Cells[i, 3].Value.ToString();
                        tempSheet.lockers.Add(tempLocker);
                    }
                    classBox.Items.Add(tempSheet.Name);
                    dataManager.LockerSheets.Add(tempSheet);
                }
                if (dataManager.LockerSheets.Count > 0)
                {
                    dataManager.currentSheet = 0;
                    classBox.SelectedItem = dataManager.LockerSheets[0].Name;
                    nameBox_TextChanged(null, null);
                }
                else
                {
                    MessageBox.Show("There aren't any locker rooms");
                }
                

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if(saveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                foreach (var ls in dataManager.LockerSheets)
                {
                    ls.lockers = ls.lockers.OrderBy(z => z.ID).ToList();
                }
                var package = new ExcelPackage();


                var wb = package.Workbook;
                for (int x = 0; x < dataManager.LockerSheets.Count; x++)
                {


                    wb.Worksheets.Add(dataManager.LockerSheets[x].Name);
                    var ws = wb.Worksheets[x];

                    ws.Cells[1, 1].Value = "Locker_Room_File";
                    ws.Cells[2, 1].Value = dataManager.LockerSheets[x].Name;
                    ws.Cells[3, 1].Value = "Čislo";
                    ws.Cells[3, 2].Value = "Meno";
                    ws.Cells[3, 3].Value = "Trieda";
                    ws.Cells[3, 4].Value = "Suradnice";

                    ws.Cells[1, 1, 3, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[1, 1, 3, 7].Style.Font.Bold = true;

                    ws.Cells["A1:D1"].Merge = true;
                    ws.Cells["A1:D1"].Style.Locked = true;
                    ws.Cells["A2:D2"].Merge = true;
                    ws.Cells["A2:D2"].Style.Locked = true;
                    ws.Columns[2].Width = 25;
                    ws.Columns[4].Width = 11;

                    for (int i = 0; i < dataManager.LockerSheets[x].lockers.Count; i++)
                    {
                        Locker tempL = dataManager.LockerSheets[x].lockers[i];
                        ws.Cells[i + 4, 1].Value = tempL.ID.ToString();
                        ws.Cells[i + 4, 2].Value = tempL.NameOfHolder;
                        ws.Cells[i + 4, 3].Value = tempL.HolderClass;
                        ws.Cells[i + 4, 4].Value = tempL.Coords[0].ToString() + "," + tempL.Coords[1].ToString();
                    }

                }
                package.SaveAs(saveFileDialog1.FileName);
                

            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
        }


        private void classBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeLockerRoom(classBox.SelectedIndex);
            nameBox_TextChanged(null, null);
        }
        private void newRoom_Click(object sender, EventArgs e)
        {
            string name = "New locker room";
            while(dataManager.LockerSheets.Any(x => x.Name == name))
            {
                name += "_";
            }
            dataManager.LockerSheets.Add(new LockerSheet(name));
            changeLockerRoom(dataManager.LockerSheets.Count-1);
            classBox.Items.Add(name);
            classBox.SelectedItem = name;

        }
        private void deleteRoom_Click(object sender, EventArgs e)
        {
            if (dataManager.LockerSheets.Count <= 1) { return; }
            if (MessageBox.Show( $"Do you really want to delete current locker room named {dataManager.LockerSheets[dataManager.currentSheet].Name} ?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataManager.LockerSheets.RemoveAt(dataManager.currentSheet);
                classBox.Items.RemoveAt(dataManager.currentSheet);
                dataManager.currentSheet = dataManager.LockerSheets.Count>1 ? dataManager.currentSheet-1 : 0;
                changeLockerRoom(dataManager.currentSheet);
                classBox.SelectedIndex = dataManager.currentSheet;

            }
        }
        private void changeLockerRoom(int index)
        {
            if (dataManager.LockerSheets.Count < 1) { return; }
            dataManager.currentSheet = index;
            panel1.Controls.Clear();
            foreach (Locker lck in dataManager.LockerSheets[dataManager.currentSheet].lockers)
            {
                this.printNewLocker(lck.ID, lck.Coords, lck.NameOfHolder == "" && lck.HolderClass == "");
            }
            
        }

        private void classBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (classBox.Text == "" || dataManager.LockerSheets.Any(x => x.Name == classBox.Text)){ classBox.Text = classBox.Items[dataManager.currentSheet].ToString() ; return; }
                classBox.Text = classBox.Text.Trim();
                classBox.Items[dataManager.currentSheet] = classBox.Text;
                dataManager.LockerSheets[dataManager.currentSheet].Name = classBox.Text;
            }
        }


        // WINDOW DESINGN
        bool MouseHold = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MouseHold) { this.Location = new System.Drawing.Point(LastObjectCoords.X - (CursorMouseCoords.X - System.Windows.Forms.Cursor.Position.X), LastObjectCoords.Y - (CursorMouseCoords.Y - System.Windows.Forms.Cursor.Position.Y)); }
            else if(MouseHoldLocker) {
                if (Control.MouseButtons != MouseButtons.Left)
                {
                    locker_MouseUp();
                    return;
                }
                selectedLocker.Location = getCoordsOfLocker();
                selectedLabel.Location = new System.Drawing.Point(selectedLocker.Location.X + 8, selectedLocker.Location.Y + 60);
            }
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CursorMouseCoords = System.Windows.Forms.Cursor.Position;
            LastObjectCoords = Location;
            MouseHold = true;
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
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
            System.Windows.Forms.Application.Exit();
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
        }

    }
}