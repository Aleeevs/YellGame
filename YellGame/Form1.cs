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
        bool down = false;
        bool left = false;
        bool right = false;

        private void timer_Tick(object sender, EventArgs e) {
            if (right && ball.Left < this.Width - ball.Width) {
                ball.Left += Math.Min(10, this.Width - ball.Width - ball.Left);
            } else if (left && ball.Left > 0) {
                ball.Left -= Math.Min(10, ball.Left);
            } 
            
            if (up && ball.Top > 0) {
                ball.Top -= Math.Min(10, ball.Top);
            } else if (down && ball.Top + ball.Height < this.Height) {
                ball.Top += Math.Min(10, this.Height - ball.Height - ball.Top);
            }

            label.Text = ball.Left + ", " + ball.Top + "\n" +
                this.Width + ", " + this.Height;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                    up = true;
                    break;
                case Keys.Down:
                    down = true;
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
                case Keys.Down:
                    down = false;
                    break;
                case Keys.Right:
                    right = false;
                    break;
                case Keys.Left:
                    left = false;
                    break;
            }
        }
    }
}
