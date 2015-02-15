namespace RobertArtificialCopilot
{
    partial class rac
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EngineComboBox = new System.Windows.Forms.ComboBox();
            this.engineLabel = new System.Windows.Forms.Label();
            this.VoiceComboBox = new System.Windows.Forms.ComboBox();
            this.VoiceLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LoadProfileButton = new System.Windows.Forms.Button();
            this.ProfileNameLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // EngineComboBox
            // 
            this.EngineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EngineComboBox.FormattingEnabled = true;
            this.EngineComboBox.Location = new System.Drawing.Point(138, 23);
            this.EngineComboBox.Name = "EngineComboBox";
            this.EngineComboBox.Size = new System.Drawing.Size(331, 21);
            this.EngineComboBox.TabIndex = 1;
            this.EngineComboBox.SelectedIndexChanged += new System.EventHandler(this.EngineComboBox_SelectedIndexChanged);
            // 
            // engineLabel
            // 
            this.engineLabel.AutoSize = true;
            this.engineLabel.Location = new System.Drawing.Point(28, 26);
            this.engineLabel.Name = "engineLabel";
            this.engineLabel.Size = new System.Drawing.Size(73, 13);
            this.engineLabel.TabIndex = 2;
            this.engineLabel.Text = "Select Engine";
            // 
            // VoiceComboBox
            // 
            this.VoiceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VoiceComboBox.FormattingEnabled = true;
            this.VoiceComboBox.Location = new System.Drawing.Point(138, 64);
            this.VoiceComboBox.Name = "VoiceComboBox";
            this.VoiceComboBox.Size = new System.Drawing.Size(331, 21);
            this.VoiceComboBox.TabIndex = 3;
            this.VoiceComboBox.SelectedIndexChanged += new System.EventHandler(this.VoiceComboBox_SelectedIndexChanged);
            // 
            // VoiceLabel
            // 
            this.VoiceLabel.AutoSize = true;
            this.VoiceLabel.Location = new System.Drawing.Point(28, 67);
            this.VoiceLabel.Name = "VoiceLabel";
            this.VoiceLabel.Size = new System.Drawing.Size(67, 13);
            this.VoiceLabel.TabIndex = 4;
            this.VoiceLabel.Text = "Select Voice";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "XML File (*.xml)|*.xml";
            this.openFileDialog1.InitialDirectory = ".\\";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // LoadProfileButton
            // 
            this.LoadProfileButton.Location = new System.Drawing.Point(31, 121);
            this.LoadProfileButton.Name = "LoadProfileButton";
            this.LoadProfileButton.Size = new System.Drawing.Size(75, 23);
            this.LoadProfileButton.TabIndex = 5;
            this.LoadProfileButton.Text = "Load Profile";
            this.LoadProfileButton.UseVisualStyleBackColor = true;
            this.LoadProfileButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LoadProfileButton_Click);
            // 
            // ProfileNameLabel
            // 
            this.ProfileNameLabel.AutoSize = true;
            this.ProfileNameLabel.Location = new System.Drawing.Point(135, 126);
            this.ProfileNameLabel.Name = "ProfileNameLabel";
            this.ProfileNameLabel.Size = new System.Drawing.Size(33, 13);
            this.ProfileNameLabel.TabIndex = 6;
            this.ProfileNameLabel.Text = "None";
            // 
            // StartButton
            // 
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(93, 388);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 7;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(394, 388);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 8;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(173, 204);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(220, 115);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // rac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 459);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ProfileNameLabel);
            this.Controls.Add(this.LoadProfileButton);
            this.Controls.Add(this.VoiceLabel);
            this.Controls.Add(this.VoiceComboBox);
            this.Controls.Add(this.engineLabel);
            this.Controls.Add(this.EngineComboBox);
            this.Name = "rac";
            this.Text = "Robert Artificial Copilot";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox EngineComboBox;
        private System.Windows.Forms.Label engineLabel;
        private System.Windows.Forms.ComboBox VoiceComboBox;
        private System.Windows.Forms.Label VoiceLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button LoadProfileButton;
        private System.Windows.Forms.Label ProfileNameLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.RichTextBox richTextBox1;



    }
}

