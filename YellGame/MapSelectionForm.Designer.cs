namespace YellGame {
    partial class MapSelectionForm {
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
            this.levelBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // levelBox
            // 
            this.levelBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(15)))), ((int)(((byte)(26)))));
            this.levelBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.levelBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.levelBox.ForeColor = System.Drawing.Color.White;
            this.levelBox.FormattingEnabled = true;
            this.levelBox.ItemHeight = 46;
            this.levelBox.Location = new System.Drawing.Point(90, 134);
            this.levelBox.Name = "levelBox";
            this.levelBox.ScrollAlwaysVisible = true;
            this.levelBox.Size = new System.Drawing.Size(193, 230);
            this.levelBox.TabIndex = 3;
            this.levelBox.TabStop = false;
            this.levelBox.UseTabStops = false;
            this.levelBox.SelectedIndexChanged += new System.EventHandler(this.levelBox_SelectedIndexChanged);
            // 
            // MapSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(15)))), ((int)(((byte)(26)))));
            this.BackgroundImage = global::YellGame.Properties.Resources.levelbackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(350, 499);
            this.Controls.Add(this.levelBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MapSelectionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "YellGame";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox levelBox;
    }
}