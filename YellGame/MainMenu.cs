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
            GameMap map = new GameMap("Abracadabra")
                .AddObstacle(Width - 300, 150)
                .AddVoid(150)
                .AddObstacle(120, 200)
                .AddVoid(120)
                .AddObstacle(120, 375)
                .AddVoid(120)
                .AddObstacle(120, 320)
                .AddVoid(200)
                .AddObstacle(120, 210)
                .AddVoid(230)
                .AddObstacle(120, 300)
                .AddVoid(300)
                .AddObstacle(120, 310)
                .AddVoid(150);
            map.SetEnd(255);

            GameForm app = new GameForm(map);
            app.Show();
            Hide();
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            Settings.OpenAndExecuteOnClose((s, e) => { });
        }
    }
}
