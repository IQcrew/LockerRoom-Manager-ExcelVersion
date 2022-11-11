﻿using System.Collections.Generic;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.newLockerButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ImportBackup = new System.Windows.Forms.Button();
            this.ExportBackup = new System.Windows.Forms.Button();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.classBox = new System.Windows.Forms.ComboBox();
            this.deleteRoom = new System.Windows.Forms.Button();
            this.newRoom = new System.Windows.Forms.Button();
            this.multipleSelection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLockersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deteleLockersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileCtrlSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileCtrlSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.windowCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveFileCtrlSToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.multipleSelection.SuspendLayout();
            this.searchMenu.SuspendLayout();
            this.windowCMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ForeColor = System.Drawing.Color.Snow;
            this.panel1.Location = new System.Drawing.Point(219, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1229, 761);
            this.panel1.TabIndex = 12;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("Bahnschrift Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 35);
            this.label1.TabIndex = 11;
            this.label1.Text = "NEW PROJECT";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(6, 764);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(207, 45);
            this.button3.TabIndex = 28;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.exitProgram);
            // 
            // newLockerButton
            // 
            this.newLockerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.newLockerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newLockerButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newLockerButton.ForeColor = System.Drawing.Color.White;
            this.newLockerButton.Location = new System.Drawing.Point(6, 48);
            this.newLockerButton.Name = "newLockerButton";
            this.newLockerButton.Size = new System.Drawing.Size(207, 45);
            this.newLockerButton.TabIndex = 27;
            this.newLockerButton.Text = "New locker";
            this.newLockerButton.UseVisualStyleBackColor = false;
            this.newLockerButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.label2.Font = new System.Drawing.Font("Bahnschrift Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(67, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 35);
            this.label2.TabIndex = 29;
            this.label2.Text = "SEARCH";
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.nameBox.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nameBox.ForeColor = System.Drawing.Color.Transparent;
            this.nameBox.Location = new System.Drawing.Point(6, 219);
            this.nameBox.MaxLength = 24;
            this.nameBox.Multiline = true;
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(207, 38);
            this.nameBox.TabIndex = 41;
            this.nameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            this.nameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.listBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(6, 263);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(207, 365);
            this.listBox1.TabIndex = 42;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
            // 
            // ImportBackup
            // 
            this.ImportBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.ImportBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportBackup.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ImportBackup.ForeColor = System.Drawing.Color.White;
            this.ImportBackup.Location = new System.Drawing.Point(6, 131);
            this.ImportBackup.Name = "ImportBackup";
            this.ImportBackup.Size = new System.Drawing.Size(99, 33);
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
            this.ExportBackup.Location = new System.Drawing.Point(114, 131);
            this.ExportBackup.Name = "ExportBackup";
            this.ExportBackup.Size = new System.Drawing.Size(99, 33);
            this.ExportBackup.TabIndex = 44;
            this.ExportBackup.Text = "Export file";
            this.ExportBackup.UseVisualStyleBackColor = false;
            this.ExportBackup.Click += new System.EventHandler(this.ExportFile_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.pictureBox7.Image = global::LockerRoom_Manager.Properties.Resources.export_icon;
            this.pictureBox7.Location = new System.Drawing.Point(149, 99);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(26, 26);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 46;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.pictureBox6.Image = global::LockerRoom_Manager.Properties.Resources.import_icon;
            this.pictureBox6.Location = new System.Drawing.Point(46, 99);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(26, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 45;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.pictureBox5.Image = global::LockerRoom_Manager.Properties.Resources._140351;
            this.pictureBox5.Location = new System.Drawing.Point(10, 8);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(25, 25);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(27)))), ((int)(((byte)(36)))));
            this.pictureBox4.Location = new System.Drawing.Point(-2, 39);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1502, 3);
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.pictureBox3.Image = global::LockerRoom_Manager.Properties.Resources.x_close_icon_white;
            this.pictureBox3.Location = new System.Drawing.Point(1432, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
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
            this.pictureBox2.Location = new System.Drawing.Point(1405, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
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
            this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1487, 43);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // classBox
            // 
            this.classBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(69)))), ((int)(((byte)(82)))));
            this.classBox.Font = new System.Drawing.Font("Bahnschrift Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.classBox.ForeColor = System.Drawing.Color.White;
            this.classBox.FormattingEnabled = true;
            this.classBox.Location = new System.Drawing.Point(6, 666);
            this.classBox.Name = "classBox";
            this.classBox.Size = new System.Drawing.Size(207, 41);
            this.classBox.TabIndex = 48;
            this.classBox.SelectedIndexChanged += new System.EventHandler(this.classBox_SelectedIndexChanged);
            this.classBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.classBox_KeyPress);
            // 
            // deleteRoom
            // 
            this.deleteRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.deleteRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteRoom.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteRoom.ForeColor = System.Drawing.Color.White;
            this.deleteRoom.Location = new System.Drawing.Point(6, 713);
            this.deleteRoom.Name = "deleteRoom";
            this.deleteRoom.Size = new System.Drawing.Size(99, 45);
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
            this.newRoom.Location = new System.Drawing.Point(114, 713);
            this.newRoom.Name = "newRoom";
            this.newRoom.Size = new System.Drawing.Size(99, 45);
            this.newRoom.TabIndex = 50;
            this.newRoom.Text = "New room";
            this.newRoom.UseVisualStyleBackColor = false;
            this.newRoom.Click += new System.EventHandler(this.newRoom_Click);
            // 
            // multipleSelection
            // 
            this.multipleSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLockersToolStripMenuItem,
            this.deteleLockersToolStripMenuItem,
            this.deselectToolStripMenuItem,
            this.saveFileCtrlSToolStripMenuItem});
            this.multipleSelection.Name = "multipleSelection";
            this.multipleSelection.Size = new System.Drawing.Size(160, 92);
            this.multipleSelection.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.multipleSelection_ItemClicked);
            // 
            // clearLockersToolStripMenuItem
            // 
            this.clearLockersToolStripMenuItem.Name = "clearLockersToolStripMenuItem";
            this.clearLockersToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.clearLockersToolStripMenuItem.Text = "Clear lockers";
            // 
            // deteleLockersToolStripMenuItem
            // 
            this.deteleLockersToolStripMenuItem.Name = "deteleLockersToolStripMenuItem";
            this.deteleLockersToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deteleLockersToolStripMenuItem.Text = "Delete lockers";
            // 
            // deselectToolStripMenuItem
            // 
            this.deselectToolStripMenuItem.Name = "deselectToolStripMenuItem";
            this.deselectToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deselectToolStripMenuItem.Text = "Deselect";
            // 
            // saveFileCtrlSToolStripMenuItem
            // 
            this.saveFileCtrlSToolStripMenuItem.Name = "saveFileCtrlSToolStripMenuItem";
            this.saveFileCtrlSToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveFileCtrlSToolStripMenuItem.Text = "Save file   Ctrl+S";
            this.saveFileCtrlSToolStripMenuItem.Click += new System.EventHandler(this.saveFileCtrlSToolStripMenuItem2_Click);
            // 
            // searchMenu
            // 
            this.searchMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLockerToolStripMenuItem,
            this.saveFileCtrlSToolStripMenuItem1});
            this.searchMenu.Name = "searchMenu";
            this.searchMenu.Size = new System.Drawing.Size(157, 48);
            // 
            // clearLockerToolStripMenuItem
            // 
            this.clearLockerToolStripMenuItem.Name = "clearLockerToolStripMenuItem";
            this.clearLockerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.clearLockerToolStripMenuItem.Text = "Clear locker";
            this.clearLockerToolStripMenuItem.Click += new System.EventHandler(this.clearLockerToolStripMenuItem_Click);
            // 
            // saveFileCtrlSToolStripMenuItem1
            // 
            this.saveFileCtrlSToolStripMenuItem1.Name = "saveFileCtrlSToolStripMenuItem1";
            this.saveFileCtrlSToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.saveFileCtrlSToolStripMenuItem1.Text = "Save file  Ctrl+S";
            this.saveFileCtrlSToolStripMenuItem1.Click += new System.EventHandler(this.saveFileCtrlSToolStripMenuItem2_Click);
            // 
            // windowCMS
            // 
            this.windowCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFileCtrlSToolStripMenuItem2});
            this.windowCMS.Name = "windowCMS";
            this.windowCMS.Size = new System.Drawing.Size(159, 26);
            // 
            // saveFileCtrlSToolStripMenuItem2
            // 
            this.saveFileCtrlSToolStripMenuItem2.Name = "saveFileCtrlSToolStripMenuItem2";
            this.saveFileCtrlSToolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.saveFileCtrlSToolStripMenuItem2.Text = "Save File  Ctrl+S";
            this.saveFileCtrlSToolStripMenuItem2.Click += new System.EventHandler(this.saveFileCtrlSToolStripMenuItem2_Click);
            // 
            // adminWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1455, 821);
            this.Controls.Add(this.newRoom);
            this.Controls.Add(this.deleteRoom);
            this.Controls.Add(this.classBox);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.ExportBackup);
            this.Controls.Add(this.ImportBackup);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
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
            this.Name = "adminWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.adminWindow_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.adminWindow_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.multipleSelection.ResumeLayout(false);
            this.searchMenu.ResumeLayout(false);
            this.windowCMS.ResumeLayout(false);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button newLockerButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button ImportBackup;
        private System.Windows.Forms.Button ExportBackup;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox classBox;
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
    }
}