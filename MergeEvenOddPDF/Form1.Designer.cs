using System.ComponentModel;

namespace MergeEvenOddPDF
{
    partial class MainForm
    {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 

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
            this.chooseFilesBtn = new System.Windows.Forms.Button();
            this.Combine = new System.Windows.Forms.Button();
            this.interweave = new System.Windows.Forms.CheckBox();
            this.Up = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            this.RemoveSelected = new System.Windows.Forms.Button();
            this.Credits = new System.Windows.Forms.LinkLabel();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // chooseFilesBtn
            // 
            this.chooseFilesBtn.Location = new System.Drawing.Point(12, 12);
            this.chooseFilesBtn.Name = "chooseFilesBtn";
            this.chooseFilesBtn.Size = new System.Drawing.Size(236, 25);
            this.chooseFilesBtn.TabIndex = 0;
            this.chooseFilesBtn.Text = "Choose File(s)";
            this.chooseFilesBtn.UseVisualStyleBackColor = true;
            this.chooseFilesBtn.Click += new System.EventHandler(this.chooseFilesBtn_Click);
            // 
            // Combine
            // 
            this.Combine.Enabled = false;
            this.Combine.Location = new System.Drawing.Point(12, 168);
            this.Combine.Name = "Combine";
            this.Combine.Size = new System.Drawing.Size(118, 25);
            this.Combine.TabIndex = 3;
            this.Combine.Text = "Combine";
            this.Combine.UseVisualStyleBackColor = true;
            this.Combine.Click += new System.EventHandler(this.Combine_Click);
            // 
            // interweave
            // 
            this.interweave.AutoSize = true;
            this.interweave.Location = new System.Drawing.Point(12, 143);
            this.interweave.Name = "interweave";
            this.interweave.Size = new System.Drawing.Size(83, 19);
            this.interweave.TabIndex = 4;
            this.interweave.Text = "Interweave";
            this.interweave.UseVisualStyleBackColor = true;
            // 
            // Up
            // 
            this.Up.Enabled = false;
            this.Up.Location = new System.Drawing.Point(254, 43);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(118, 25);
            this.Up.TabIndex = 5;
            this.Up.Text = "Up";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // Down
            // 
            this.Down.Enabled = false;
            this.Down.Location = new System.Drawing.Point(254, 74);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(118, 25);
            this.Down.TabIndex = 6;
            this.Down.Text = "Down";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.Down_Click);
            // 
            // RemoveSelected
            // 
            this.RemoveSelected.Enabled = false;
            this.RemoveSelected.Location = new System.Drawing.Point(254, 105);
            this.RemoveSelected.Name = "RemoveSelected";
            this.RemoveSelected.Size = new System.Drawing.Size(118, 25);
            this.RemoveSelected.TabIndex = 7;
            this.RemoveSelected.Text = "Remove Selected";
            this.RemoveSelected.UseVisualStyleBackColor = true;
            this.RemoveSelected.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // Credits
            // 
            this.Credits.AutoSize = true;
            this.Credits.Location = new System.Drawing.Point(12, 196);
            this.Credits.Name = "Credits";
            this.Credits.Size = new System.Drawing.Size(133, 15);
            this.Credits.TabIndex = 9;
            this.Credits.TabStop = true;
            this.Credits.Text = "Created by Sem Postma";
            this.Credits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Credits_LinkClicked);
            // 
            // filesListBox
            // 
            this.filesListBox.DisplayMember = "Label";
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.ItemHeight = 15;
            this.filesListBox.Location = new System.Drawing.Point(12, 43);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size(236, 94);
            this.filesListBox.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 215);
            this.Controls.Add(this.filesListBox);
            this.Controls.Add(this.Credits);
            this.Controls.Add(this.RemoveSelected);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.interweave);
            this.Controls.Add(this.Combine);
            this.Controls.Add(this.chooseFilesBtn);
            this.Name = "MainForm";
            this.Text = "PDF Interweaved Merger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button chooseFilesBtn;
        private Button Combine;
        private CheckBox interweave;
        private Button Up;
        private Button Down;
        private Button RemoveSelected;
        private LinkLabel Credits;
        private ListBox filesListBox;
    }
}