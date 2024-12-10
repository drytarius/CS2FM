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
            this.fontlist = new System.Windows.Forms.ListBox();
            this.addfontbtn = new System.Windows.Forms.Button();
            this.removefontbtn = new System.Windows.Forms.Button();
            this.applyfontbtn = new System.Windows.Forms.Button();
            this.directorygroup = new System.Windows.Forms.GroupBox();
            this.autodetectbtn = new System.Windows.Forms.Button();
            this.opengamepathtbn = new System.Windows.Forms.Button();
            this.opencorepathbtn = new System.Windows.Forms.Button();
            this.gamepath = new System.Windows.Forms.TextBox();
            this.corepath = new System.Windows.Forms.TextBox();
            this.gamedirlabel = new System.Windows.Forms.Label();
            this.coredirlabel = new System.Windows.Forms.Label();
            this.fontPreviewTextBox = new System.Windows.Forms.TextBox();
            this.sliderFontSize = new System.Windows.Forms.TrackBar();
            this.fontSizeLabel = new System.Windows.Forms.Label();
            this.resetfontbtn = new System.Windows.Forms.Button();
            this.downbtn = new System.Windows.Forms.Button();
            this.upbtn = new System.Windows.Forms.Button();
            this.directorygroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // corebtn
            // 
            this.corebtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.corebtn.Location = new System.Drawing.Point(144, 230);
            this.corebtn.Name = "corebtn";
            this.corebtn.Size = new System.Drawing.Size(84, 20);
            this.corebtn.TabIndex = 1;
            this.corebtn.Text = "Select Core Dir";
            this.corebtn.UseVisualStyleBackColor = true;
            this.corebtn.Click += new System.EventHandler(this.corebtn_Click);
            // 
            // gamebtn
            // 
            this.gamebtn.Location = new System.Drawing.Point(234, 230);
            this.gamebtn.Name = "gamebtn";
            this.gamebtn.Size = new System.Drawing.Size(84, 20);
            this.gamebtn.TabIndex = 2;
            this.gamebtn.Text = "Select Game Dir";
            this.gamebtn.UseVisualStyleBackColor = true;
            this.gamebtn.Click += new System.EventHandler(this.gamebtn_Click);
            // 
            // fontlist
            // 
            this.fontlist.FormattingEnabled = true;
            this.fontlist.HorizontalScrollbar = true;
            this.fontlist.Location = new System.Drawing.Point(14, 12);
            this.fontlist.Name = "fontlist";
            this.fontlist.Size = new System.Drawing.Size(124, 160);
            this.fontlist.TabIndex = 5;
            this.fontlist.SelectedIndexChanged += new System.EventHandler(this.fontlist_SelectedIndexChanged);
            // 
            // addfontbtn
            // 
            this.addfontbtn.Location = new System.Drawing.Point(12, 178);
            this.addfontbtn.Name = "addfontbtn";
            this.addfontbtn.Size = new System.Drawing.Size(39, 20);
            this.addfontbtn.TabIndex = 6;
            this.addfontbtn.Text = "Add";
            this.addfontbtn.UseVisualStyleBackColor = true;
            this.addfontbtn.Click += new System.EventHandler(this.addfontbtn_Click);
            // 
            // removefontbtn
            // 
            this.removefontbtn.Location = new System.Drawing.Point(51, 178);
            this.removefontbtn.Name = "removefontbtn";
            this.removefontbtn.Size = new System.Drawing.Size(39, 20);
            this.removefontbtn.TabIndex = 7;
            this.removefontbtn.Text = "Rem";
            this.removefontbtn.UseVisualStyleBackColor = true;
            this.removefontbtn.Click += new System.EventHandler(this.removefontbtn_Click);
            // 
            // applyfontbtn
            // 
            this.applyfontbtn.Location = new System.Drawing.Point(12, 421);
            this.applyfontbtn.Name = "applyfontbtn";
            this.applyfontbtn.Size = new System.Drawing.Size(315, 28);
            this.applyfontbtn.TabIndex = 8;
            this.applyfontbtn.Text = "Apply Selected Font";
            this.applyfontbtn.UseVisualStyleBackColor = true;
            this.applyfontbtn.Click += new System.EventHandler(this.applyfontbtn_Click);
            // 
            // directorygroup
            // 
            this.directorygroup.Controls.Add(this.autodetectbtn);
            this.directorygroup.Controls.Add(this.opengamepathtbn);
            this.directorygroup.Controls.Add(this.opencorepathbtn);
            this.directorygroup.Controls.Add(this.gamepath);
            this.directorygroup.Controls.Add(this.corepath);
            this.directorygroup.Controls.Add(this.gamedirlabel);
            this.directorygroup.Controls.Add(this.coredirlabel);
            this.directorygroup.Location = new System.Drawing.Point(144, 12);
            this.directorygroup.Name = "directorygroup";
            this.directorygroup.Size = new System.Drawing.Size(183, 212);
            this.directorygroup.TabIndex = 9;
            this.directorygroup.TabStop = false;
            this.directorygroup.Text = "Directories:";
            // 
            // autodetectbtn
            // 
            this.autodetectbtn.Location = new System.Drawing.Point(62, 183);
            this.autodetectbtn.Name = "autodetectbtn";
            this.autodetectbtn.Size = new System.Drawing.Size(115, 23);
            this.autodetectbtn.TabIndex = 10;
            this.autodetectbtn.Text = "Auto Detect Paths";
            this.autodetectbtn.UseVisualStyleBackColor = true;
            this.autodetectbtn.Click += new System.EventHandler(this.autodetectbtn_Click);
            // 
            // opengamepathtbn
            // 
            this.opengamepathtbn.FlatAppearance.BorderSize = 0;
            this.opengamepathtbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.opengamepathtbn.Image = global::CS2FM.Properties.Resources.folder;
            this.opengamepathtbn.Location = new System.Drawing.Point(9, 100);
            this.opengamepathtbn.Name = "opengamepathtbn";
            this.opengamepathtbn.Size = new System.Drawing.Size(22, 20);
            this.opengamepathtbn.TabIndex = 9;
            this.opengamepathtbn.UseVisualStyleBackColor = true;
            this.opengamepathtbn.Click += new System.EventHandler(this.opengamepathtbn_Click);
            // 
            // opencorepathbtn
            // 
            this.opencorepathbtn.BackColor = System.Drawing.SystemColors.Control;
            this.opencorepathbtn.FlatAppearance.BorderSize = 0;
            this.opencorepathbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.opencorepathbtn.Image = global::CS2FM.Properties.Resources.folder;
            this.opencorepathbtn.Location = new System.Drawing.Point(8, 48);
            this.opencorepathbtn.Name = "opencorepathbtn";
            this.opencorepathbtn.Size = new System.Drawing.Size(23, 20);
            this.opencorepathbtn.TabIndex = 8;
            this.opencorepathbtn.UseVisualStyleBackColor = false;
            this.opencorepathbtn.Click += new System.EventHandler(this.opencorepathbtn_Click);
            // 
            // gamepath
            // 
            this.gamepath.Location = new System.Drawing.Point(37, 101);
            this.gamepath.Name = "gamepath";
            this.gamepath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.gamepath.Size = new System.Drawing.Size(140, 20);
            this.gamepath.TabIndex = 7;
            this.gamepath.TextChanged += new System.EventHandler(this.gamepath_TextChanged);
            // 
            // corepath
            // 
            this.corepath.Location = new System.Drawing.Point(37, 49);
            this.corepath.Name = "corepath";
            this.corepath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.corepath.Size = new System.Drawing.Size(140, 20);
            this.corepath.TabIndex = 6;
            this.corepath.TextChanged += new System.EventHandler(this.corepath_TextChanged);
            // 
            // gamedirlabel
            // 
            this.gamedirlabel.AutoSize = true;
            this.gamedirlabel.Location = new System.Drawing.Point(6, 84);
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
            // fontPreviewTextBox
            // 
            this.fontPreviewTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.fontPreviewTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fontPreviewTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.fontPreviewTextBox.Location = new System.Drawing.Point(14, 268);
            this.fontPreviewTextBox.Multiline = true;
            this.fontPreviewTextBox.Name = "fontPreviewTextBox";
            this.fontPreviewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.fontPreviewTextBox.Size = new System.Drawing.Size(313, 113);
            this.fontPreviewTextBox.TabIndex = 13;
            this.fontPreviewTextBox.Text = "The quick brown fox jumps over the lazy dog.";
            // 
            // sliderFontSize
            // 
            this.sliderFontSize.LargeChange = 50;
            this.sliderFontSize.Location = new System.Drawing.Point(12, 204);
            this.sliderFontSize.Maximum = 300;
            this.sliderFontSize.Minimum = 25;
            this.sliderFontSize.Name = "sliderFontSize";
            this.sliderFontSize.Size = new System.Drawing.Size(126, 45);
            this.sliderFontSize.SmallChange = 25;
            this.sliderFontSize.TabIndex = 14;
            this.sliderFontSize.Value = 100;
            this.sliderFontSize.Scroll += new System.EventHandler(this.sliderFontSize_Scroll);
            // 
            // fontSizeLabel
            // 
            this.fontSizeLabel.AutoSize = true;
            this.fontSizeLabel.Location = new System.Drawing.Point(12, 237);
            this.fontSizeLabel.Name = "fontSizeLabel";
            this.fontSizeLabel.Size = new System.Drawing.Size(25, 13);
            this.fontSizeLabel.TabIndex = 15;
            this.fontSizeLabel.Text = "x.xx";
            // 
            // resetfontbtn
            // 
            this.resetfontbtn.Location = new System.Drawing.Point(12, 387);
            this.resetfontbtn.Name = "resetfontbtn";
            this.resetfontbtn.Size = new System.Drawing.Size(315, 28);
            this.resetfontbtn.TabIndex = 16;
            this.resetfontbtn.Text = "Reset Font";
            this.resetfontbtn.UseVisualStyleBackColor = true;
            this.resetfontbtn.Click += new System.EventHandler(this.resetfontbtn_Click);
            // 
            // downbtn
            // 
            this.downbtn.Image = global::CS2FM.Properties.Resources.bullet_arrow_down;
            this.downbtn.Location = new System.Drawing.Point(117, 178);
            this.downbtn.Name = "downbtn";
            this.downbtn.Size = new System.Drawing.Size(21, 20);
            this.downbtn.TabIndex = 11;
            this.downbtn.UseVisualStyleBackColor = true;
            this.downbtn.Click += new System.EventHandler(this.downbtn_Click);
            // 
            // upbtn
            // 
            this.upbtn.Image = global::CS2FM.Properties.Resources.bullet_arrow_up;
            this.upbtn.Location = new System.Drawing.Point(96, 178);
            this.upbtn.Name = "upbtn";
            this.upbtn.Size = new System.Drawing.Size(21, 20);
            this.upbtn.TabIndex = 10;
            this.upbtn.UseVisualStyleBackColor = true;
            this.upbtn.Click += new System.EventHandler(this.upbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 458);
            this.Controls.Add(this.resetfontbtn);
            this.Controls.Add(this.fontSizeLabel);
            this.Controls.Add(this.fontPreviewTextBox);
            this.Controls.Add(this.downbtn);
            this.Controls.Add(this.sliderFontSize);
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
            ((System.ComponentModel.ISupportInitialize)(this.sliderFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button corebtn;
        private Button gamebtn;
        private ListBox fontlist;
        private Button addfontbtn;
        private Button removefontbtn;
        private Button applyfontbtn;
        private GroupBox directorygroup;
        private Label coredirlabel;
        private Label gamedirlabel;
        private Button upbtn;
        private Button downbtn;
        private TextBox corepath;
        private TextBox gamepath;
        private Button opencorepathbtn;
        private Button opengamepathtbn;
        private TextBox fontPreviewTextBox;
        private TrackBar sliderFontSize;
        private Label fontSizeLabel;
        private Button resetfontbtn;
        private Button autodetectbtn;
    }
}
