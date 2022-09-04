using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Runtime.InteropServices;

namespace YellGame {
    public partial class GameForm : Form {

        bool stop = false;
        int up = 0;
        int right = 0;

        GameMap map;
        WaveInEvent waveIn;

        public GameForm() {
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

            waveIn.DeviceNumber = Settings.Device;

            int horizontal = 0;
            int vertical = 0;
            bool moveAll = false;

            if (this.right != 0 && player.Right < 780) {
                horizontal = this.right;
                moveAll = player.Right >= Width * 7 / 10 && map.GetLastObstacle().Right != Width;
            }
            
            if (this.up != 0 && player.Top > 0) {
                vertical = -this.up;
            } else if (player.Top < 452) {
                vertical = 4;
            }

            int top = player.Top;
            int bottom = player.Bottom;
            int right = player.Right;
            Rectangle playerRect = new Rectangle(player.Location, player.Size);

            bool win = false;
            bool dead = false;
            foreach (var obstacle in map.Obstacles) {
                if (up == 0 && playerRect.IntersectsWith(new Rectangle(obstacle.Left, obstacle.Top - 1, obstacle.Width, vertical))) {
                    vertical = obstacle.Top - bottom;
                    if (obstacle.Deadly) 
                        dead = true;
                    if (map.GetLastObstacle().Equals(obstacle))
                        win = true;
                }
                else if (up != 0 && playerRect.IntersectsWith(new Rectangle(obstacle.Left, obstacle.Bottom + vertical + 1, obstacle.Width, -vertical))) {
                    vertical = obstacle.Bottom - top;
                    if (obstacle.Deadly)
                        dead = true;
                }
                else if (this.right != 0 && playerRect.IntersectsWith(new Rectangle(obstacle.Left - 1, obstacle.Top, horizontal, obstacle.Height))) {
                    horizontal = obstacle.Left - right;
                    moveAll = false;
                    if (obstacle.Deadly)
                        dead = true;
                }
            }

            if (win) {
                stop = true;
                this.winLabel.Visible = true;
                return;
            }

            if (moveAll)
                horizontal = 0;

            player.Left += horizontal;
            player.Top += vertical;

            if (moveAll) {
                map.MoveAll();
            }

            if (player.Bottom >= 510 || dead) {
                stop = true;
                loseLabel.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            foreach (var ob in map.Obstacles)
                Controls.Add(ob.Picture);

            waveIn = new WaveInEvent();
            waveIn.DataAvailable += ShowPeakMono;
            waveIn.StartRecording();
        }

        private void ShowPeakMono(object sender, WaveInEventArgs args) {
            float maxValue = 32767;
            int peakValue = 0;
            int bytesPerSample = 2;
            for (int index = 0; index < args.BytesRecorded; index += bytesPerSample) {
                int value = BitConverter.ToInt16(args.Buffer, index);
                peakValue = Math.Max(peakValue, value);
            }

            maxValue += (int) (Settings.Sensibility / 100 * maxValue);
            float volume = (100 * peakValue) / maxValue;

            up = (int) volume * 11 / 100;
            right = (int) volume * 13 / 100;
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            stop = true;
            Settings.OpenAndExecuteOnClose((s, e) => stop = false);
        }
    }
}
