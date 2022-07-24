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
        public Form1() {
            InitializeComponent();
        }

        bool up = false;
        bool left = false;
        bool right = false;

        List<Obstacle> obstacles = new List<Obstacle>();

        private void timer_Tick(object sender, EventArgs e) {
            int movement = 2;
            int horizontal = 0;
            int vertical = 0;
            bool moveAll = false;

            if (this.right && player.Right < 780) {
                horizontal = movement;
                moveAll = player.Right >= Width * 7 / 10;
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

            Dictionary<Obstacle, ObstacleSide> sides = new Dictionary<Obstacle, ObstacleSide>();
            foreach (var obstacle in obstacles) {
                if (IsBetween(bottom, obstacle.Top, obstacle.Top + vertical) && (IsBetween(left, obstacle.Left, obstacle.Right) || IsBetween(right, obstacle.Left, obstacle.Right))) {
                    vertical = obstacle.Top - bottom;
                    sides.Add(obstacle, ObstacleSide.TOP);
                }
                else if (IsBetween(top, obstacle.Bottom + vertical, obstacle.Bottom) && (IsBetween(left, obstacle.Left, obstacle.Right) || IsBetween(right, obstacle.Left, obstacle.Right))) {
                    vertical = obstacle.Bottom - top;
                    sides.Add(obstacle, ObstacleSide.BOTTOM);
                }
                else if (IsBetween(right, obstacle.Left, obstacle.Left + horizontal) && (IsBetween(top, obstacle.Top, obstacle.Bottom) || IsBetween(bottom, obstacle.Top, obstacle.Bottom) || IsBetween(obstacle.Top, top, bottom) || IsBetween(obstacle.Bottom, top, bottom))) {
                    horizontal = obstacle.Left - right;
                    sides.Add(obstacle, ObstacleSide.LEFT);
                }
                else if (IsBetween(left, obstacle.Right + horizontal, obstacle.Right) && (IsBetween(top, obstacle.Top, obstacle.Bottom) || IsBetween(bottom, obstacle.Top, obstacle.Bottom) || IsBetween(obstacle.Top, top, bottom) || IsBetween(obstacle.Bottom, top, bottom))) {
                    horizontal = obstacle.Right - left;
                    sides.Add(obstacle, ObstacleSide.RIGHT);
                }
            }

            if (moveAll)
                horizontal = 0;

            player.Left += horizontal;
            player.Top += vertical;

            if (moveAll && !sides.ContainsValue(ObstacleSide.LEFT)) {
                foreach (var obstacle in obstacles) {
                    if (!obstacle.Locked)
                        obstacle.Picture.Left -= 2;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var side in sides)
                sb.Append(side.Value).Append(' ');

            Text = sb.ToString() + player.Left + ", " + player.Top + " | " + horizontal + ", " + vertical;
        }

        private bool IsBetween(int n, int a, int b) {
            return n >= a && n <= b;
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
            obstacles.Add(new Obstacle(100, 100, 100, 100));
            obstacles.Add(new Obstacle(200, 100, 47, 31));
            obstacles.Add(new Obstacle(0, Height - 150, Width - 300, 150));
            obstacles.Add(new Obstacle(0, Height - 150, 75, 150));
            obstacles.Add(new Obstacle(Width - 200, Height - 300, 90, 300));

            foreach (var ob in obstacles)
                Controls.Add(ob.Picture);
        }
    }
}
