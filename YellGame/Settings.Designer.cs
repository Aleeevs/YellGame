
namespace YellGame {
    partial class Settings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.micLabel = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.sensibilityBar = new System.Windows.Forms.TrackBar();
            this.sensibilityLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sensibilityBar)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDevice
            // 
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(129, 119);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(171, 28);
            this.cbDevice.TabIndex = 1;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
            // 
            // micLabel
            // 
            this.micLabel.AutoSize = true;
            this.micLabel.Location = new System.Drawing.Point(173, 96);
            this.micLabel.Name = "micLabel";
            this.micLabel.Size = new System.Drawing.Size(85, 20);
            this.micLabel.TabIndex = 2;
            this.micLabel.Text = "Input audio";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Arial Rounded MT Bold", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.title.Location = new System.Drawing.Point(70, 20);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(296, 43);
            this.title.TabIndex = 3;
            this.title.Text = "IMPOSTAZIONI";
            // 
            // sensibilityBar
            // 
            this.sensibilityBar.BackColor = System.Drawing.Color.White;
            this.sensibilityBar.Location = new System.Drawing.Point(31, 239);
            this.sensibilityBar.Maximum = 200;
            this.sensibilityBar.Name = "sensibilityBar";
            this.sensibilityBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.sensibilityBar.RightToLeftLayout = true;
            this.sensibilityBar.Size = new System.Drawing.Size(382, 56);
            this.sensibilityBar.TabIndex = 6;
            this.sensibilityBar.TabStop = false;
            this.sensibilityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sensibilityBar.Scroll += new System.EventHandler(this.sensibilityBar_Scroll);
            // 
            // sensibilityLabel
            // 
            this.sensibilityLabel.AutoSize = true;
            this.sensibilityLabel.Location = new System.Drawing.Point(129, 203);
            this.sensibilityLabel.Name = "sensibilityLabel";
            this.sensibilityLabel.Size = new System.Drawing.Size(175, 20);
            this.sensibilityLabel.TabIndex = 7;
            this.sensibilityLabel.Text = "Sensibilità del microfono";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 370);
            this.Controls.Add(this.sensibilityLabel);
            this.Controls.Add(this.sensibilityBar);
            this.Controls.Add(this.title);
            this.Controls.Add(this.micLabel);
            this.Controls.Add(this.cbDevice);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.sensibilityBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Label micLabel;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.TrackBar sensibilityBar;
        private System.Windows.Forms.Label sensibilityLabel;
    }
}