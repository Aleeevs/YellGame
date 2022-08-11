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
    public partial class Form1 : Form {

        bool stop = false;
        bool up = false;
        bool left = false;
        bool right = false;

        GameMap map;

        public Form1() {
            InitializeComponent();

            map = new GameMap("Abracadabra")
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
        }

        private void timer_Tick(object sender, EventArgs e) {
            if (stop) return;

            int movement = 2;
            int horizontal = 0;
            int vertical = 0;
            bool moveAll = false;

            if (this.right && player.Right < 780) {
                horizontal = movement;
                moveAll = player.Right >= Width * 7 / 10 && map.GetLastObstacle().Right != Width;
            } else if (this.left && player.Left > 0) {
                horizontal = -movement;
            }
            
            if (up && player.Top > 0) {
                vertical = -movement;
            } else if (player.Top < 452) {
                vertical = movement * 3 / 2;
            }

            int top = player.Top;
            int bottom = player.Bottom;
            int left = player.Left;
            int right = player.Right;
            Rectangle playerRect = new Rectangle(player.Location, player.Size);

            Dictionary<Obstacle, ObstacleSide> sides = new Dictionary<Obstacle, ObstacleSide>();
            foreach (var obstacle in map.Obstacles) {
                if (!up && playerRect.IntersectsWith(new Rectangle(obstacle.Left, obstacle.Top - 1, obstacle.Width, vertical))) {
                    vertical = obstacle.Top - bottom;
                    sides.Add(obstacle, ObstacleSide.TOP);
                }
                else if (up && playerRect.IntersectsWith(new Rectangle(obstacle.Left, obstacle.Bottom + vertical + 1, obstacle.Width, -vertical))) {
                    vertical = obstacle.Bottom - top;
                    sides.Add(obstacle, ObstacleSide.BOTTOM);
                }
                else if (this.right && playerRect.IntersectsWith(new Rectangle(obstacle.Left - 1, obstacle.Top, horizontal, obstacle.Height))) {
                    horizontal = obstacle.Left - right;
                    sides.Add(obstacle, ObstacleSide.LEFT);
                }
                else if (this.left && playerRect.IntersectsWith(new Rectangle(obstacle.Right + horizontal + 1, obstacle.Top, -horizontal, obstacle.Height))) {
                    horizontal = obstacle.Right - left;
                    sides.Add(obstacle, ObstacleSide.RIGHT);
                }
            }

            bool win = sides.GetValueOrDefault(map.GetLastObstacle(), ObstacleSide.NONE) == ObstacleSide.TOP;
            if (win) {
                stop = true;
                this.winLabel.Visible = true;
                return;
            }

            if (moveAll)
                horizontal = 0;

            player.Left += horizontal;
            player.Top += vertical;

            if (moveAll && !sides.ContainsValue(ObstacleSide.LEFT)) {
                map.MoveAll();
            }

            StringBuilder sb = new StringBuilder();
            foreach (var side in sides)
                sb.Append(side.Value).Append(' ');

            Text = sb.ToString() + player.Left + ", " + player.Top + " | " + horizontal + ", " + vertical;

            if (player.Bottom >= 510) {
                stop = true;
                loseLabel.Visible = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                    up = true;
                    break;
                case Keys.Right:
                    right = true;
                    break;
                case Keys.Left:
                    left = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                    up = false;
                    break;
                case Keys.Right:
                    right = false;
                    break;
                case Keys.Left:
                    left = false;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            foreach (var ob in map.Obstacles)
                Controls.Add(ob.Picture);
        }
    }
}
