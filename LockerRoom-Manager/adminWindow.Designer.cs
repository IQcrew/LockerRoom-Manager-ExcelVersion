using System.Collections.Generic;

namespace LockerRoom_Manager
{
    partial class adminWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region graphics of lockers
        
        public Dictionary<System.Windows.Forms.PictureBox, System.Windows.Forms.Label> LockersNumbers = new Dictionary<System.Windows.Forms.PictureBox, System.Windows.Forms.Label>();
        public void printNewLocker(int num, int[] coords, bool empty)
        {
            System.Windows.Forms.PictureBox tempPictureBox = new System.Windows.Forms.PictureBox();

            tempPictureBox.Image = empty ? global::LockerRoom_Manager.Properties.Resources.openLocker : global::LockerRoom_Manager.Properties.Resources.closeLocker;
            tempPictureBox.Location = new System.Drawing.Point(coords[0], coords[1]);
            tempPictureBox.Size = new System.Drawing.Size(38, 89);
            tempPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            tempPictureBox.TabIndex = 0;
            tempPictureBox.TabStop = false;
            tempPictureBox.Click += new System.EventHandler(this.lockerSelection_Click);
            tempPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.locker_MouseDown);
            tempPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.openLockerProperties);
            tempPictureBox.LocationChanged += new System.EventHandler(this.locker_LocationChanged);
            tempPictureBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            tempPictureBox.Name = num.ToString();
            this.panel1.Controls.Add(tempPictureBox);
            tempPictureBox.BringToFront();

            System.Windows.Forms.Label tempLabel = new System.Windows.Forms.Label();
            tempLabel.AutoSize = true;
            tempLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(161)))), ((int)(((byte)(221)))));
            tempLabel.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            tempLabel.Location = new System.Drawing.Point(coords[0] + 8,coords[1] + 60);
            tempLabel.Size = new System.Drawing.Size(26, 19);
            tempLabel.TabIndex = 1;
            tempLabel.Click += new System.EventHandler(this.lockerSelection_Click);
            tempLabel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.openLockerProperties);
            tempLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.locker_MouseDown);
            tempLabel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            tempLabel.Text = num.ToString();
            this.panel1.Controls.Add(tempLabel);
            tempLabel.BringToFront();

            LockersNumbers.Add(tempPictureBox, tempLabel);

        }
        public void earseLocker(int id)
        {
            System.Windows.Forms.PictureBox lckr = getLockerPictureBox(id);
            if(lckr == null) { return; }

            System.Windows.Forms.Label lbl = LockersNumbers[lckr];
            lckr.Hide();
            lbl.Hide();
            panel1.Controls.Remove(lbl);
            panel1.Controls.Remove(lckr);
            panel1.Refresh();
            LockersNumbers.Remove(lckr);

        }
        public void LockerState(int id,bool open) { 
            System.Windows.Forms.PictureBox lckr = getLockerPictureBox(id);
            if (lckr == null) { return; }
            lckr.Image = open ? global::LockerRoom_Manager.Properties.Resources.openLocker : global::LockerRoom_Manager.Properties.Resources.closeLocker;

        }
        private System.Windows.Forms.PictureBox getLockerPictureBox(int id)
        {
            foreach (System.Windows.Forms.PictureBox lckr in LockersNumbers.Keys)
            {
                if (lckr.Name == id.ToString())
                {
                    return lckr;
                }
            }
            return null;
        }
        public void creteNewlockerPB()
        {
            System.Windows.Forms.PictureBox tempNewLockerPictureB = new System.Windows.Forms.PictureBox();
            tempNewLockerPictureB.BackColor = System.Drawing.Color.Transparent;
            tempNewLockerPictureB.Image = global::LockerRoom_Manager.Properties.Resources.locker;
            tempNewLockerPictureB.Location = new System.Drawing.Point(0, 0);
            tempNewLockerPictureB.Name = "NewLockerPictureB";
            tempNewLockerPictureB.Size = new System.Drawing.Size(38, 89);
            tempNewLockerPictureB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            tempNewLockerPictureB.TabIndex = 0;
            tempNewLockerPictureB.TabStop = false;
            tempNewLockerPictureB.Visible = false;
            panel1.Controls.Add(tempNewLockerPictureB);
            this.NewLockerPictureB = tempNewLockerPictureB;
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminWindow));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.newLockerButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.filterBox = new System.Windows.Forms.TextBox();
            this.filterResutlsBox = new System.Windows.Forms.ListBox();
            this.ImportBackup = new System.Windows.Forms.Button();
            this.ExportBackup = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chooseRoomBox = new System.Windows.Forms.ComboBox();
            this.deleteRoom = new System.Windows.Forms.Button();
            this.newRoom = new System.Windows.Forms.Button();
            this.multipleSelection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLockersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deteleLockersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileCtrlSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileCtrlSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.windowCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveFileCtrlSToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NewLockerPictureB = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.multipleSelection.SuspendLayout();
            this.searchMenu.SuspendLayout();
            this.windowCMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewLockerPictureB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("Bahnschrift Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(55, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 45);
            this.label1.TabIndex = 11;
            this.label1.Text = "NEW PROJECT";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Location = new System.Drawing.Point(8, 940);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(276, 55);
            this.exitButton.TabIndex = 28;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitProgram);
            // 
            // newLockerButton
            // 
            this.newLockerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.newLockerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newLockerButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newLockerButton.ForeColor = System.Drawing.Color.White;
            this.newLockerButton.Location = new System.Drawing.Point(8, 59);
            this.newLockerButton.Margin = new System.Windows.Forms.Padding(4);
            this.newLockerButton.Name = "newLockerButton";
            this.newLockerButton.Size = new System.Drawing.Size(276, 55);
            this.newLockerButton.TabIndex = 27;
            this.newLockerButton.Text = "New locker mode";
            this.newLockerButton.UseVisualStyleBackColor = false;
            this.newLockerButton.Click += new System.EventHandler(this.newLockerModeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.label2.Font = new System.Drawing.Font("Bahnschrift Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(89, 223);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 45);
            this.label2.TabIndex = 29;
            this.label2.Text = "SEARCH";
            // 
            // filterBox
            // 
            this.filterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.filterBox.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.filterBox.ForeColor = System.Drawing.Color.Transparent;
            this.filterBox.Location = new System.Drawing.Point(8, 270);
            this.filterBox.Margin = new System.Windows.Forms.Padding(4);
            this.filterBox.MaxLength = 24;
            this.filterBox.Multiline = true;
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(275, 46);
            this.filterBox.TabIndex = 41;
            this.filterBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.filterBox.TextChanged += new System.EventHandler(this.filter_TextChanged);
            this.filterBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // filterResutlsBox
            // 
            this.filterResutlsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.filterResutlsBox.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.filterResutlsBox.ForeColor = System.Drawing.Color.White;
            this.filterResutlsBox.FormattingEnabled = true;
            this.filterResutlsBox.ItemHeight = 24;
            this.filterResutlsBox.Location = new System.Drawing.Point(8, 324);
            this.filterResutlsBox.Margin = new System.Windows.Forms.Padding(4);
            this.filterResutlsBox.Name = "filterResutlsBox";
            this.filterResutlsBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.filterResutlsBox.Size = new System.Drawing.Size(275, 484);
            this.filterResutlsBox.TabIndex = 42;
            this.filterResutlsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.filterListBox_MouseDoubleClick);
            this.filterResutlsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.searchBox_MouseDown);
            // 
            // ImportBackup
            // 
            this.ImportBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.ImportBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportBackup.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ImportBackup.ForeColor = System.Drawing.Color.White;
            this.ImportBackup.Location = new System.Drawing.Point(8, 161);
            this.ImportBackup.Margin = new System.Windows.Forms.Padding(4);
            this.ImportBackup.Name = "ImportBackup";
            this.ImportBackup.Size = new System.Drawing.Size(132, 41);
            this.ImportBackup.TabIndex = 43;
            this.ImportBackup.Text = "Import file";
            this.ImportBackup.UseVisualStyleBackColor = false;
            this.ImportBackup.Click += new System.EventHandler(this.ImportFile_Click);
            // 
            // ExportBackup
            // 
            this.ExportBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.ExportBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportBackup.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ExportBackup.ForeColor = System.Drawing.Color.White;
            this.ExportBackup.Location = new System.Drawing.Point(152, 161);
            this.ExportBackup.Margin = new System.Windows.Forms.Padding(4);
            this.ExportBackup.Name = "ExportBackup";
            this.ExportBackup.Size = new System.Drawing.Size(132, 41);
            this.ExportBackup.TabIndex = 44;
            this.ExportBackup.Text = "Export file";
            this.ExportBackup.UseVisualStyleBackColor = false;
            this.ExportBackup.Click += new System.EventHandler(this.ExportFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // chooseRoomBox
            // 
            this.chooseRoomBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.chooseRoomBox.Font = new System.Drawing.Font("Bahnschrift Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chooseRoomBox.ForeColor = System.Drawing.Color.White;
            this.chooseRoomBox.FormattingEnabled = true;
            this.chooseRoomBox.Location = new System.Drawing.Point(8, 820);
            this.chooseRoomBox.Margin = new System.Windows.Forms.Padding(4);
            this.chooseRoomBox.Name = "chooseRoomBox";
            this.chooseRoomBox.Size = new System.Drawing.Size(275, 49);
            this.chooseRoomBox.TabIndex = 48;
            this.chooseRoomBox.SelectedIndexChanged += new System.EventHandler(this.classBox_SelectedIndexChanged);
            this.chooseRoomBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.classBox_KeyPress);
            // 
            // deleteRoom
            // 
            this.deleteRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.deleteRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteRoom.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteRoom.ForeColor = System.Drawing.Color.White;
            this.deleteRoom.Location = new System.Drawing.Point(8, 878);
            this.deleteRoom.Margin = new System.Windows.Forms.Padding(4);
            this.deleteRoom.Name = "deleteRoom";
            this.deleteRoom.Size = new System.Drawing.Size(132, 55);
            this.deleteRoom.TabIndex = 49;
            this.deleteRoom.Text = "Delete room";
            this.deleteRoom.UseVisualStyleBackColor = false;
            this.deleteRoom.Click += new System.EventHandler(this.deleteRoom_Click);
            // 
            // newRoom
            // 
            this.newRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.newRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newRoom.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newRoom.ForeColor = System.Drawing.Color.White;
            this.newRoom.Location = new System.Drawing.Point(152, 878);
            this.newRoom.Margin = new System.Windows.Forms.Padding(4);
            this.newRoom.Name = "newRoom";
            this.newRoom.Size = new System.Drawing.Size(132, 55);
            this.newRoom.TabIndex = 50;
            this.newRoom.Text = "New room";
            this.newRoom.UseVisualStyleBackColor = false;
            this.newRoom.Click += new System.EventHandler(this.newRoom_Click);
            // 
            // multipleSelection
            // 
            this.multipleSelection.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.multipleSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLockersToolStripMenuItem,
            this.deteleLockersToolStripMenuItem,
            this.newLockerToolStripMenuItem,
            this.saveFileCtrlSToolStripMenuItem,
            this.deselectToolStripMenuItem});
            this.multipleSelection.Name = "multipleSelection";
            this.multipleSelection.Size = new System.Drawing.Size(232, 124);
            this.multipleSelection.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.multipleSelection_ItemClicked);
            // 
            // clearLockersToolStripMenuItem
            // 
            this.clearLockersToolStripMenuItem.Name = "clearLockersToolStripMenuItem";
            this.clearLockersToolStripMenuItem.Size = new System.Drawing.Size(231, 24);
            this.clearLockersToolStripMenuItem.Text = "Clear selected lockers";
            // 
            // deteleLockersToolStripMenuItem
            // 
            this.deteleLockersToolStripMenuItem.Name = "deteleLockersToolStripMenuItem";
            this.deteleLockersToolStripMenuItem.Size = new System.Drawing.Size(231, 24);
            this.deteleLockersToolStripMenuItem.Text = "Delete selected lockers";
            // 
            // newLockerToolStripMenuItem
            // 
            this.newLockerToolStripMenuItem.Name = "newLockerToolStripMenuItem";
            this.newLockerToolStripMenuItem.Size = new System.Drawing.Size(231, 24);
            this.newLockerToolStripMenuItem.Text = "New locker";
            // 
            // saveFileCtrlSToolStripMenuItem
            // 
            this.saveFileCtrlSToolStripMenuItem.Name = "saveFileCtrlSToolStripMenuItem";
            this.saveFileCtrlSToolStripMenuItem.Size = new System.Drawing.Size(231, 24);
            this.saveFileCtrlSToolStripMenuItem.Text = "Save file   Ctrl+S";
            this.saveFileCtrlSToolStripMenuItem.Click += new System.EventHandler(this.saveFileCtrlSToolStripMenuItem2_Click);
            // 
            // deselectToolStripMenuItem
            // 
            this.deselectToolStripMenuItem.Name = "deselectToolStripMenuItem";
            this.deselectToolStripMenuItem.Size = new System.Drawing.Size(231, 24);
            this.deselectToolStripMenuItem.Text = "Deselect";
            // 
            // searchMenu
            // 
            this.searchMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.searchMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLockerToolStripMenuItem,
            this.saveFileCtrlSToolStripMenuItem1});
            this.searchMenu.Name = "searchMenu";
            this.searchMenu.Size = new System.Drawing.Size(184, 52);
            // 
            // clearLockerToolStripMenuItem
            // 
            this.clearLockerToolStripMenuItem.Name = "clearLockerToolStripMenuItem";
            this.clearLockerToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.clearLockerToolStripMenuItem.Text = "Clear locker";
            this.clearLockerToolStripMenuItem.Click += new System.EventHandler(this.clearLockerToolStripMenuItem_Click);
            // 
            // saveFileCtrlSToolStripMenuItem1
            // 
            this.saveFileCtrlSToolStripMenuItem1.Name = "saveFileCtrlSToolStripMenuItem1";
            this.saveFileCtrlSToolStripMenuItem1.Size = new System.Drawing.Size(183, 24);
            this.saveFileCtrlSToolStripMenuItem1.Text = "Save file  Ctrl+S";
            this.saveFileCtrlSToolStripMenuItem1.Click += new System.EventHandler(this.saveFileCtrlSToolStripMenuItem2_Click);
            // 
            // windowCMS
            // 
            this.windowCMS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.windowCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFileCtrlSToolStripMenuItem2});
            this.windowCMS.Name = "windowCMS";
            this.windowCMS.Size = new System.Drawing.Size(186, 28);
            // 
            // saveFileCtrlSToolStripMenuItem2
            // 
            this.saveFileCtrlSToolStripMenuItem2.Name = "saveFileCtrlSToolStripMenuItem2";
            this.saveFileCtrlSToolStripMenuItem2.Size = new System.Drawing.Size(185, 24);
            this.saveFileCtrlSToolStripMenuItem2.Text = "Save File  Ctrl+S";
            this.saveFileCtrlSToolStripMenuItem2.Click += new System.EventHandler(this.saveFileCtrlSToolStripMenuItem2_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.pictureBox7.Image = global::LockerRoom_Manager.Properties.Resources.export_icon;
            this.pictureBox7.Location = new System.Drawing.Point(199, 122);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(35, 32);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 46;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.pictureBox6.Image = global::LockerRoom_Manager.Properties.Resources.import_icon;
            this.pictureBox6.Location = new System.Drawing.Point(61, 122);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(35, 32);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 45;
            this.pictureBox6.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.NewLockerPictureB);
            this.panel1.ForeColor = System.Drawing.Color.Snow;
            this.panel1.Location = new System.Drawing.Point(292, 59);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1638, 936);
            this.panel1.TabIndex = 12;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // NewLockerPictureB
            // 
            this.NewLockerPictureB.BackColor = System.Drawing.Color.Transparent;
            this.NewLockerPictureB.Image = global::LockerRoom_Manager.Properties.Resources.locker;
            this.NewLockerPictureB.Location = new System.Drawing.Point(0, 0);
            this.NewLockerPictureB.Margin = new System.Windows.Forms.Padding(4);
            this.NewLockerPictureB.Name = "NewLockerPictureB";
            this.NewLockerPictureB.Size = new System.Drawing.Size(38, 89);
            this.NewLockerPictureB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.NewLockerPictureB.TabIndex = 0;
            this.NewLockerPictureB.TabStop = false;
            this.NewLockerPictureB.Visible = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.pictureBox5.Image = global::LockerRoom_Manager.Properties.Resources._140351;
            this.pictureBox5.Location = new System.Drawing.Point(13, 10);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(33, 31);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(27)))), ((int)(((byte)(36)))));
            this.pictureBox4.Location = new System.Drawing.Point(-3, 48);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(2003, 4);
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.pictureBox3.Image = global::LockerRoom_Manager.Properties.Resources.x_close_icon_white;
            this.pictureBox3.Location = new System.Drawing.Point(1909, 2);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(27, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.exitProgram);
            this.pictureBox3.MouseEnter += new System.EventHandler(this.pictureBox3_MouseEnter);
            this.pictureBox3.MouseLeave += new System.EventHandler(this.pictureBox3_MouseLeave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.pictureBox2.Image = global::LockerRoom_Manager.Properties.Resources.winfo_icon_minimize_64;
            this.pictureBox2.Location = new System.Drawing.Point(1873, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.pictureBox1.Location = new System.Drawing.Point(-3, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1983, 53);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // adminWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1940, 1010);
            this.Controls.Add(this.newRoom);
            this.Controls.Add(this.deleteRoom);
            this.Controls.Add(this.chooseRoomBox);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.ExportBackup);
            this.Controls.Add(this.ImportBackup);
            this.Controls.Add(this.filterResutlsBox);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.newLockerButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "adminWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.adminWindow_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.adminWindow_MouseDown);
            this.multipleSelection.ResumeLayout(false);
            this.searchMenu.ResumeLayout(false);
            this.windowCMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewLockerPictureB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button newLockerButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox filterBox;
        private System.Windows.Forms.ListBox filterResutlsBox;
        private System.Windows.Forms.Button ImportBackup;
        private System.Windows.Forms.Button ExportBackup;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox chooseRoomBox;
        private System.Windows.Forms.Button deleteRoom;
        private System.Windows.Forms.Button newRoom;
        private System.Windows.Forms.ContextMenuStrip multipleSelection;
        private System.Windows.Forms.ToolStripMenuItem clearLockersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deteleLockersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip searchMenu;
        private System.Windows.Forms.ToolStripMenuItem clearLockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileCtrlSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileCtrlSToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip windowCMS;
        private System.Windows.Forms.ToolStripMenuItem saveFileCtrlSToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem newLockerToolStripMenuItem;
        private System.Windows.Forms.PictureBox NewLockerPictureB;
    }
}