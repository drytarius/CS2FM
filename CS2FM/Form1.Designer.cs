using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CS2FM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.corebtn = new System.Windows.Forms.Button();
            this.gamebtn = new System.Windows.Forms.Button();
            this.corepath = new System.Windows.Forms.Label();
            this.gamepath = new System.Windows.Forms.Label();
            this.fontlist = new System.Windows.Forms.ListBox();
            this.addfontbtn = new System.Windows.Forms.Button();
            this.removefontbtn = new System.Windows.Forms.Button();
            this.applyfontbtn = new System.Windows.Forms.Button();
            this.directorygroup = new System.Windows.Forms.GroupBox();
            this.gamedirlabel = new System.Windows.Forms.Label();
            this.coredirlabel = new System.Windows.Forms.Label();
            this.upbtn = new System.Windows.Forms.Button();
            this.downbtn = new System.Windows.Forms.Button();
            this.directorygroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // corebtn
            // 
            this.corebtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.corebtn.Location = new System.Drawing.Point(149, 208);
            this.corebtn.Name = "corebtn";
            this.corebtn.Size = new System.Drawing.Size(84, 20);
            this.corebtn.TabIndex = 1;
            this.corebtn.Text = "Select Core Dir";
            this.corebtn.UseVisualStyleBackColor = true;
            this.corebtn.Click += new System.EventHandler(this.corebtn_Click);
            // 
            // gamebtn
            // 
            this.gamebtn.Location = new System.Drawing.Point(243, 208);
            this.gamebtn.Name = "gamebtn";
            this.gamebtn.Size = new System.Drawing.Size(84, 20);
            this.gamebtn.TabIndex = 2;
            this.gamebtn.Text = "Select Game Dir";
            this.gamebtn.UseVisualStyleBackColor = true;
            this.gamebtn.Click += new System.EventHandler(this.gamebtn_Click);
            // 
            // corepath
            // 
            this.corepath.AutoEllipsis = true;
            this.corepath.AutoSize = true;
            this.corepath.Location = new System.Drawing.Point(5, 46);
            this.corepath.Name = "corepath";
            this.corepath.Size = new System.Drawing.Size(16, 13);
            this.corepath.TabIndex = 3;
            this.corepath.Text = "...";
            // 
            // gamepath
            // 
            this.gamepath.AutoEllipsis = true;
            this.gamepath.AutoSize = true;
            this.gamepath.Location = new System.Drawing.Point(5, 84);
            this.gamepath.Name = "gamepath";
            this.gamepath.Size = new System.Drawing.Size(16, 13);
            this.gamepath.TabIndex = 4;
            this.gamepath.Text = "...";
            // 
            // fontlist
            // 
            this.fontlist.FormattingEnabled = true;
            this.fontlist.Location = new System.Drawing.Point(10, 10);
            this.fontlist.Name = "fontlist";
            this.fontlist.Size = new System.Drawing.Size(129, 160);
            this.fontlist.TabIndex = 5;
            // 
            // addfontbtn
            // 
            this.addfontbtn.Location = new System.Drawing.Point(12, 175);
            this.addfontbtn.Name = "addfontbtn";
            this.addfontbtn.Size = new System.Drawing.Size(39, 20);
            this.addfontbtn.TabIndex = 6;
            this.addfontbtn.Text = "Add";
            this.addfontbtn.UseVisualStyleBackColor = true;
            this.addfontbtn.Click += new System.EventHandler(this.addfontbtn_Click);
            // 
            // removefontbtn
            // 
            this.removefontbtn.Location = new System.Drawing.Point(51, 175);
            this.removefontbtn.Name = "removefontbtn";
            this.removefontbtn.Size = new System.Drawing.Size(39, 20);
            this.removefontbtn.TabIndex = 7;
            this.removefontbtn.Text = "Rem";
            this.removefontbtn.UseVisualStyleBackColor = true;
            this.removefontbtn.Click += new System.EventHandler(this.removefontbtn_Click);
            // 
            // applyfontbtn
            // 
            this.applyfontbtn.Location = new System.Drawing.Point(12, 237);
            this.applyfontbtn.Name = "applyfontbtn";
            this.applyfontbtn.Size = new System.Drawing.Size(315, 28);
            this.applyfontbtn.TabIndex = 8;
            this.applyfontbtn.Text = "Apply Selected Font";
            this.applyfontbtn.UseVisualStyleBackColor = true;
            this.applyfontbtn.Click += new System.EventHandler(this.applyfontbtn_Click);
            // 
            // directorygroup
            // 
            this.directorygroup.Controls.Add(this.gamedirlabel);
            this.directorygroup.Controls.Add(this.coredirlabel);
            this.directorygroup.Controls.Add(this.corepath);
            this.directorygroup.Controls.Add(this.gamepath);
            this.directorygroup.Location = new System.Drawing.Point(144, 12);
            this.directorygroup.Name = "directorygroup";
            this.directorygroup.Size = new System.Drawing.Size(183, 191);
            this.directorygroup.TabIndex = 9;
            this.directorygroup.TabStop = false;
            this.directorygroup.Text = "Directories:";
            // 
            // gamedirlabel
            // 
            this.gamedirlabel.AutoSize = true;
            this.gamedirlabel.Location = new System.Drawing.Point(5, 71);
            this.gamedirlabel.Name = "gamedirlabel";
            this.gamedirlabel.Size = new System.Drawing.Size(83, 13);
            this.gamedirlabel.TabIndex = 5;
            this.gamedirlabel.Text = "Game Directory:";
            // 
            // coredirlabel
            // 
            this.coredirlabel.AutoSize = true;
            this.coredirlabel.Location = new System.Drawing.Point(5, 33);
            this.coredirlabel.Name = "coredirlabel";
            this.coredirlabel.Size = new System.Drawing.Size(77, 13);
            this.coredirlabel.TabIndex = 4;
            this.coredirlabel.Text = "Core Directory:";
            // 
            // upbtn
            // 
            this.upbtn.Image = global::CS2FM.Properties.Resources.ico_up;
            this.upbtn.Location = new System.Drawing.Point(96, 175);
            this.upbtn.Name = "upbtn";
            this.upbtn.Size = new System.Drawing.Size(21, 20);
            this.upbtn.TabIndex = 10;
            this.upbtn.UseVisualStyleBackColor = true;
            this.upbtn.Click += new System.EventHandler(this.upbtn_Click);
            // 
            // downbtn
            // 
            this.downbtn.Image = global::CS2FM.Properties.Resources.ico_down;
            this.downbtn.Location = new System.Drawing.Point(118, 175);
            this.downbtn.Name = "downbtn";
            this.downbtn.Size = new System.Drawing.Size(21, 20);
            this.downbtn.TabIndex = 11;
            this.downbtn.UseVisualStyleBackColor = true;
            this.downbtn.Click += new System.EventHandler(this.downbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 276);
            this.Controls.Add(this.downbtn);
            this.Controls.Add(this.upbtn);
            this.Controls.Add(this.directorygroup);
            this.Controls.Add(this.applyfontbtn);
            this.Controls.Add(this.corebtn);
            this.Controls.Add(this.removefontbtn);
            this.Controls.Add(this.addfontbtn);
            this.Controls.Add(this.fontlist);
            this.Controls.Add(this.gamebtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "CS2FM";
            this.directorygroup.ResumeLayout(false);
            this.directorygroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button corebtn;
        private Button gamebtn;
        private Label corepath;
        private Label gamepath;
        private ListBox fontlist;
        private Button addfontbtn;
        private Button removefontbtn;
        private Button applyfontbtn;
        private GroupBox directorygroup;
        private Label coredirlabel;
        private Label gamedirlabel;
        private Button upbtn;
        private Button downbtn;
    }
}
