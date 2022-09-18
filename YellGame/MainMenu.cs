using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YellGame {
    public partial class MainMenu : Form {

        public static MainMenu Instance;

        public MainMenu() {
            InitializeComponent();
            Instance = this;

            title.Font = new Font(Data.CustomFonts.Families[0], 50);

            playButton.Font = new Font(Data.CustomFonts.Families[0], 25);
            settingsButton.Font = new Font(Data.CustomFonts.Families[0], 8);
            exitButton.Font = new Font(Data.CustomFonts.Families[0], 15);

            title.Left = (Width - title.Width) / 2;
            playButton.Left = (Width - playButton.Width) / 2;
            settingsButton.Left = Width / 2 - settingsButton.Width;
            exitButton.Left = Width / 2;
        }

        private void playButton_Click(object sender, EventArgs e) {
            MapSelectionForm app = new MapSelectionForm();
            app.ShowDialog();
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            Settings.OpenAndExecuteOnClose((s, e) => { });
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) {
            Data.SaveTimeRecords();
            Application.Exit();
        }

        private void exitButton_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
