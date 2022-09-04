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
            GameForm app = new GameForm();
            app.Show();
            Hide();
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            Settings.OpenAndExecuteOnClose((s, e) => { });
        }
    }
}
