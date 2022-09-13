using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YellGame {
    public partial class MainMenu : Form {

        public MainMenu() {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e) {
            GameMap map = Data.maps.FirstOrDefault();
            if (map == null) {
                Console.WriteLine("Errore.");
                Application.Exit();
                return;
            }

            GameForm app = new GameForm(map);
            app.Show();
            Hide();
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            Settings.OpenAndExecuteOnClose((s, e) => { });
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) {
            Data.SaveTimeRecords();
            Application.Exit();
        }
    }
}
