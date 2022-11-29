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
using Microsoft.Vbe.Interop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Net.Http.Headers;
using Microsoft.Office.Interop.Excel;


namespace LockerRoom_Manager
{
    public partial class adminWindow : Form
    {
        string openFile = "";
        bool newLockerMode = false;
        bool MouseHoldLocker = false;
        System.Drawing.Point CursorMouseCoords = new System.Drawing.Point();
        System.Drawing.Point LastObjectCoords = new System.Drawing.Point();
        PictureBox selectedLocker = new PictureBox();
        System.Windows.Forms.Label selectedLabel = new System.Windows.Forms.Label();
        List<int> selectedLockers = new List<int>();
        public List<int> openLockerTabs = new List<int>();


        public adminWindow()
        {
            InitializeComponent();
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            filter_TextChanged(null, null);
            saveFileDialog1.Filter = "Data Files (*.xlsx)|*.xlsx";
            saveFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "Data Files (*.xlsx)|*.xlsx";
            openFileDialog1.AddExtension = true;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.KeyPreview = true;
        }

        #region events
        private void newLockerModeButton_Click(object sender, EventArgs e)
        {
            if (dataManager.LockerSheets.Count < 1) {return; }
            changeNewLockerMode(!newLockerMode);
        }
        private void locker_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { multipleSelection.Show(System.Windows.Forms.Cursor.Position); }
            if (sender.GetType().ToString() == "System.Windows.Forms.PictureBox")
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
            if (!selectedLockers.Contains(Int32.Parse(selectedLocker.Name))) { selectedLocker.BorderStyle = BorderStyle.None; }
            MouseHoldLocker = false;
            if(selectedLocker.Location == LastObjectCoords) { return; }
            if (possiblePos(selectedLocker.Location.X, selectedLocker.Location.Y))
            {
                Locker tempLocker = dataManager.FindLocker(Int32.Parse(selectedLabel.Text));
                tempLocker.Coords = new int[] { selectedLocker.Location.X-panel1.AutoScrollPosition.X, selectedLocker.Location.Y- panel1.AutoScrollPosition.Y };
                if (!selectedLockers.Contains(Int32.Parse(selectedLabel.Text))) { selectedLocker.BorderStyle = BorderStyle.None; }

            }
            else
            {
                selectedLocker.Location = LastObjectCoords;
                selectedLabel.Location = new System.Drawing.Point(selectedLocker.Location.X + 8, selectedLocker.Location.Y + 60);
            }

        }
        private void locker_LocationChanged(object sender, EventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left) {
                selectedLocker.BorderStyle = BorderStyle.Fixed3D;
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


        private void filterListBox_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                int temp = Int32.Parse(filterResutlsBox.SelectedItem.ToString().Split(' ')[0]);
                if (openLockerTabs.Contains(temp)) { return; }
                Form LockerPropertiesForm = new LockerPropertiesAdminWindow(temp);
                LockerPropertiesForm.Owner = this;
                LockerPropertiesForm.Show();
                LockerPropertiesForm.Location = System.Windows.Forms.Cursor.Position;
            }
            catch { }
        }

        public void filter_TextChanged(object sender, EventArgs e)
        {
            if (dataManager.LockerSheets.Count < 1) { return; }
            filterResutlsBox.Items.Clear();
            foreach (Locker item in dataManager.currentSheet.lockers)
            {
                if ($"{item.HolderClass.ToLower()} {item.NameOfHolder.ToLower()}".Contains(filterBox.Text.ToLower())){
                    filterResutlsBox.Items.Add(item.ID.ToString() +" - "+ item.HolderClass+" - "+item.NameOfHolder);
                }
            }
        }
        private void ImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(openFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                label1.Text = openFileDialog1.SafeFileName;
                string backup = File.ReadAllText(openFileDialog1.FileName);
                dataManager.LockerSheets.Clear();
                chooseRoomBox.Items.Clear();
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
                    chooseRoomBox.Items.Add(tempSheet.Name);
                    dataManager.LockerSheets.Add(tempSheet);
                }
                if (dataManager.LockerSheets.Count > 0)
                {
                    dataManager.currentSheetIndex = 0;
                    chooseRoomBox.SelectedItem = dataManager.LockerSheets[0].Name;
                    filter_TextChanged(null, null);
                    openFile = openFileDialog1.FileName;
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

        private void adminWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {    
            if(e.Control && e.KeyCode == Keys.S)
            {
                if (openFile != "")
                    exportFile(openFile);
                else if(dataManager.LockerSheets.Count > 0)
                    ExportFile_Click(null, null);
            }
            if(e.KeyCode == Keys.Escape)
            {
                if (dataManager.LockerSheets.Count < 1) { return; }
                newLockerMode = false;
                newLockerButton.BackColor = newLockerMode ? Color.FromArgb(255, 0, 25, 35) : Color.FromArgb(255, 0, 85, 100);
                NewLockerPictureB.Visible = newLockerMode;
            }
        }
        private void ExportFile_Click(object sender, EventArgs e)
        {
            changeNewLockerMode(false);
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            openFile = saveFileDialog1.FileName;
            label1.Text = saveFileDialog1.FileName.Split('\\').Last();
            exportFile(saveFileDialog1.FileName);
        }


        private void classBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeLockerRoom(chooseRoomBox.SelectedIndex);
            filter_TextChanged(null, null);
            this.creteNewlockerPB();
        }
        private void newRoom_Click(object sender, EventArgs e)
        {
            string name = "New locker room";
            while(dataManager.LockerSheets.Any(x => x.Name == name))
            {
                name += "_";
            }
            dataManager.LockerSheets.Add(new LockerSheet(name));
            chooseRoomBox.Items.Add(name);
            chooseRoomBox.SelectedItem = name;

        }
        private void deleteRoom_Click(object sender, EventArgs e)
        {
            if (dataManager.LockerSheets.Count <= 1) { return; }
            if (MessageBox.Show( $"Do you really want to delete current locker room named {dataManager.currentSheet.Name} ?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataManager.LockerSheets.RemoveAt(dataManager.currentSheetIndex);
                chooseRoomBox.Items.RemoveAt(dataManager.currentSheetIndex);
                dataManager.currentSheetIndex = dataManager.LockerSheets.Count>1 ? dataManager.currentSheetIndex-1 : 0;
                changeLockerRoom(dataManager.currentSheetIndex);
                chooseRoomBox.SelectedIndex = dataManager.currentSheetIndex;

            }
        }


        private void classBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (chooseRoomBox.Text == "" || dataManager.LockerSheets.Any(x => x.Name == chooseRoomBox.Text)){ chooseRoomBox.Text = chooseRoomBox.Items[dataManager.currentSheetIndex].ToString() ; return; }
                chooseRoomBox.Text = chooseRoomBox.Text.Trim();
                chooseRoomBox.Items[dataManager.currentSheetIndex] = chooseRoomBox.Text;
                dataManager.currentSheet.Name = chooseRoomBox.Text;
            }
        }


        private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right) { multipleSelection.Show(System.Windows.Forms.Cursor.Position); }
        }

        private void lockerSelection_Click(object sender, EventArgs e)
        {
            if (new Keys[] { Keys.Shift, Keys.Control }.Contains(Control.ModifierKeys))
            {
                int lockerID = sender.GetType().ToString() == "System.Windows.Forms.PictureBox" ? Int32.Parse((sender as PictureBox).Name) : Int32.Parse((sender as System.Windows.Forms.Label).Text);
                System.Windows.Forms.PictureBox tempPictureBox = this.getLockerPictureBox(lockerID);
                if (selectedLockers.Contains(lockerID))
                {
                    selectedLockers.Remove(lockerID);
                    tempPictureBox.BorderStyle = BorderStyle.None;
                }
                else
                {
                    selectedLockers.Add(lockerID);
                    tempPictureBox.BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        private void multipleSelection_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dataManager.LockerSheets.Count <= 1) { return; }
            changeNewLockerMode(false);
            switch (e.ClickedItem.Text)
            {
                case "Clear selected lockers":
                    foreach (int lckrID in selectedLockers)
                    {
                        Locker tempLckr = dataManager.FindLocker(lckrID);
                        tempLckr.HolderClass = "";
                        tempLckr.NameOfHolder = "";
                        this.LockerState(lckrID, tempLckr.HolderClass == "" && tempLckr.NameOfHolder == "");
                    }
                    clearSelected();
                    break;

                case "Delete selected lockers":

                    foreach (var item in dataManager.currentSheet.lockers.Where(x => selectedLockers.Contains(x.ID)).ToList())
                    {
                        dataManager.currentSheet.lockers.Remove(item);
                    }
                    changeLockerRoom(dataManager.currentSheetIndex);
                    break;

                case "Deselect":
                    clearSelected();
                    break;
                case "New locker":
                    if(dataManager.LockerSheets.Count < 1) { break; }
                    System.Drawing.Point tempP =panel1.PointToClient(System.Windows.Forms.Cursor.Position);
                    int[] tempPos = { (int)((tempP.X/10)*10), (int)((tempP.Y / 10) * 10) };
                    if (possiblePos(tempPos[0], tempPos[1]))
                    {
                        Locker newLocker = dataManager.CreateLocker(tempPos);
                        this.printNewLocker(newLocker.ID, newLocker.Coords, true);
                    }
                    break;


                default:
                    return;
            }
        }
        private void searchBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { searchMenu.Show(System.Windows.Forms.Cursor.Position); }
        }
        private void adminWindow_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { windowCMS.Show(System.Windows.Forms.Cursor.Position); }
        }
        private void saveFileCtrlSToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataManager.LockerSheets.Count<1) { }
            else if (openFile != "") { exportFile(openFile); }
            else { ExportFile_Click(null, null); }
        }
        private void clearLockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in filterResutlsBox.SelectedItems)
            {
                int lckrID = Int32.Parse(item.ToString().Split(' ')[0]);
                Locker tempLckr = dataManager.FindLocker(lckrID);
                tempLckr.HolderClass = "";
                tempLckr.NameOfHolder = "";
                this.LockerState(lckrID, tempLckr.HolderClass == "" && tempLckr.NameOfHolder == "");
            }
            filter_TextChanged(null, null);
        }


        #endregion

        #region additional methods

        private void changeNewLockerMode(bool val)
        {
            newLockerMode = val;
            newLockerButton.BackColor = newLockerMode ? Color.FromArgb(255, 0, 25, 35) : Color.FromArgb(255, 0, 85, 100);
            NewLockerPictureB.Visible = newLockerMode;
        }

        private System.Drawing.Point getCoordsOfLocker()  // coords limits 
        {
            int x = (((LastObjectCoords.X - (CursorMouseCoords.X - System.Windows.Forms.Cursor.Position.X))-panel1.AutoScrollPosition.X)/10)*10;
            int y = (((LastObjectCoords.Y - (CursorMouseCoords.Y - System.Windows.Forms.Cursor.Position.Y))-panel1.AutoScrollPosition.Y)/10)*10;
            if (x < 0) { x = 0; }
            if (y < 0) { y = 0; }

            return new System.Drawing.Point(x,y);
        }

        private void changeLockerRoom(int index)
        {
            if (dataManager.LockerSheets.Count < 1) { return; }
            selectedLockers.Clear();
            dataManager.currentSheetIndex = index;
            panel1.Controls.Clear();
            this.LockersNumbers.Clear();
            this.creteNewlockerPB();
            newLockerMode = false;
            newLockerButton.BackColor = Color.FromArgb(255, 0, 85, 100);
            foreach (Locker lck in dataManager.currentSheet.lockers)
            {
                this.printNewLocker(lck.ID, lck.Coords, lck.NameOfHolder == "" && lck.HolderClass == "");
            }

        }

        public void clearSelected()
        {
            foreach (int lckrID in selectedLockers) { this.getLockerPictureBox(lckrID).BorderStyle = BorderStyle.None; }
            selectedLockers.Clear();
        }
        private bool possiblePos(int x, int y, bool creating = false)
        {
            foreach (PictureBox lckr in this.LockersNumbers.Keys)
            {
                if ( (selectedLabel.Text != lckr.Name || creating) && Math.Abs(lckr.Location.X - x) <=39 && Math.Abs(lckr.Location.Y - y) <= 89) { return false; }
            }

            return true;
        }

        private void exportFile(string path)
        {
            try
            {
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
                package.SaveAs(path);


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion


        #region WINDOW DESINGN
        bool MouseHold = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (newLockerMode)
            {
                System.Drawing.Point tempP = panel1.PointToClient(System.Windows.Forms.Cursor.Position);
                int[] tempPos = { (int)((tempP.X / 10) * 10), (int)((tempP.Y / 10) * 10) };
                if (tempPos[0] < 0) { tempPos[0] = 0; }
                if (tempPos[1] < 0) { tempPos[1] = 0; }
                NewLockerPictureB.Location = new System.Drawing.Point(tempPos[0], tempPos[1]);

                if (Control.MouseButtons == MouseButtons.Left && tempP.X > 0 && tempP.X > 0 && possiblePos(tempPos[0], tempPos[1], true))
                {
                    possiblePos(tempPos[0], tempPos[1]);
                    Locker newLocker = dataManager.CreateLocker(tempPos);
                    this.printNewLocker(newLocker.ID, newLocker.Coords, true);
                    filter_TextChanged(null, null);
                    NewLockerPictureB.BringToFront();
                }

            }
            else
            {
                if (MouseHold) { this.Location = new System.Drawing.Point(LastObjectCoords.X - (CursorMouseCoords.X - System.Windows.Forms.Cursor.Position.X), LastObjectCoords.Y - (CursorMouseCoords.Y - System.Windows.Forms.Cursor.Position.Y)); }
                else if (MouseHoldLocker)
                {
                    if (Control.MouseButtons != MouseButtons.Left)
                    {
                        locker_MouseUp();
                        return;
                    }
                    System.Drawing.Point newPos = getCoordsOfLocker();
                    selectedLocker.Location =  new System.Drawing.Point(newPos.X + panel1.AutoScrollPosition.X, newPos.Y + panel1.AutoScrollPosition.Y);
                    selectedLabel.Location = new System.Drawing.Point(selectedLocker.Location.X + 8, selectedLocker.Location.Y + 60);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { windowCMS.Show(System.Windows.Forms.Cursor.Position); return; }
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
            switch (MessageBox.Show($"Do you want to save your work before leaving?", "Are you sure?", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes: 
                    if(openFile == "")
                    {
                        ExportFile_Click(null, null);
                    }
                    else
                    {
                        exportFile(openFile);
                    }
                    System.Windows.Forms.Application.Exit();
                    break;
                case DialogResult.No:
                    System.Windows.Forms.Application.Exit();
                    break;
                case DialogResult.Cancel:
                    return;
            }
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
        }

        #endregion

    }
}