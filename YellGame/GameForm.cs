using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NAudio.Wave;
using YellGame.Properties;

namespace YellGame {
    public partial class GameForm : Form {

        bool stop = false;
        int up = 0;
        int right = 0;
        DateTime startTime;

        GameMap map;
        WaveInEvent waveIn;

        public GameForm(GameMap map) {
            InitializeComponent();
            this.map = map;
            Text = "YellGame | " + map.Name;
            startTime = DateTime.Now;
            player.Location = new Point(Width / 20, map.Obstacles[0].Top - 10);
        }

        private void timer_Tick(object sender, EventArgs e) {
            if (stop) return;

            // Controlla se ci sono dispositivi input collegati, altrimenti ferma il gioco
            if (WaveIn.DeviceCount == 0) {
                stop = true;
                MessageBox.Show("Nessun dispositivo di input audio trovato.");
                Settings.OpenAndExecuteOnClose((s, e) => stop = false);
                return;
            }

            // Imposta il dispositivo di input selezionato
            waveIn.DeviceNumber = Data.Device;

            int horizontal = 0; // movimento orizzontale
            int vertical = 0; // movimento verticale
            bool moveAll = false; 

            // Aumenta il movimento orizzontale se può spostarsi verso destra
            if (this.right != 0 && player.Right < 780) {
                horizontal = this.right;

                // Se il giocatore ha raggiunto i 7/10 dello schermo, allora tutti gli ostacoli dovranno scorrere
                // Effetto super mario
                moveAll = player.Right >= Width * 7 / 10 && map.GetLastObstacle().Right > Width;
            }
            
            // Aumenta/Diminuisce il movimento verticale
            // Effetto salto/gravità
            if (this.up != 0 && player.Top > 0) {
                vertical = -this.up;
            } else if (player.Top < 452) {
                vertical = 4;
            }

            // coordinate attuali del giocatore
            int top = player.Top;
            int bottom = player.Bottom;
            int right = player.Right;
            Rectangle playerRect = new Rectangle(player.Location, player.Size);

            bool win = false;
            bool dead = false;
            // Controllo se il giocatore è a contatto con un ostacolo
            // Gestione collisioni
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

            // Se il giocatore ha toccato l'ultimo ostacolo, allora ha vinto
            if (win) {
                ExecuteWin();
                return;
            }

            // Se si deve spostare la mappa a sinistra (super mario), il giocatore non si sposta
            // Spostamento della posizione del giocatore
            player.Left += moveAll ? 0 : horizontal;
            player.Top += vertical;

            // Effetto super mario
            if (moveAll)
                map.MoveAll(Math.Abs(horizontal));

            // Se il giocatore è finito nel fosso o ha toccato un ostacolo deadly, ha perso
            if (player.Bottom >= 510 || dead) {
                ExecuteLose();
                return;
            }
        }

        private void GameForm_Load(object sender, EventArgs e) {
            // Stampo tutti gli ostacoli della mappa
            foreach (var ob in map.Obstacles)
                Controls.Add(ob.Picture);

            // Inizio la ricezione del suono
            waveIn = new WaveInEvent();
            waveIn.DataAvailable += ShowPeakMono;
            waveIn.StartRecording();
        }

        private void ShowPeakMono(object sender, WaveInEventArgs args) {
            // rilevamento del suono del microfono
            float maxValue = 32767;
            int peakValue = 0;
            int bytesPerSample = 2;
            for (int index = 0; index < args.BytesRecorded; index += bytesPerSample) {
                int value = BitConverter.ToInt16(args.Buffer, index);
                peakValue = Math.Max(peakValue, value);
            }

            // calcolo del volume
            maxValue += (int) (Data.Sensibility / 100 * maxValue);
            float volume = (100 * peakValue) / maxValue;

            up = (int) volume * 8 / 100; // determina il movimento verticale
            right = (int) volume * 11 / 100; // determina il movimento orizzontale
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            // Apro le impostazioni. Quando vengono chiuse, il gioco riprende
            stop = true;
            Settings.OpenAndExecuteOnClose((s, e) => stop = false);
        }

        private void retryButton_Click(object sender, EventArgs e) {
            // Faccio ricominciare il gioco
            map.Reset();
            new GameForm(map).Show();
            Close();
        }

        private void logoutButton_Click(object sender, EventArgs e) {
            new MainMenu().Show();
            Close();
        }

        private void StopAll() {
            stop = true;
            waveIn.StopRecording();
            waveIn.Dispose();
            timer.Enabled = false;
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            StopAll();
        }

        private void ExecuteWin() {
            stop = true;
            CreateResultView("HAI VINTO!", Color.LimeGreen, true);
        }

        private void ExecuteLose() {
            stop = true;
            CreateResultView("HAI PERSO!", Color.Red);
        }

        private void CreateResultView(string text, Color color, bool showTime = false) {
            Label result = new Label {
                AutoSize = true,
                BackColor = Color.WhiteSmoke,
                Font = new Font("Impact", 72F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = color,
                Name = "resultLabel",
                TabIndex = 1,
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(result);
            result.Location = new Point((Width - result.Width) / 2, Height / 2 - result.Height);

            Button retry = new Button() {
                BackgroundImage = Properties.Resources.playagain,
                BackgroundImageLayout = ImageLayout.Stretch,
                Name = "retryButton",
                Size = new Size(45, 45),
                TabIndex = 5,
                UseVisualStyleBackColor = true
            };
            retry.Click += retryButton_Click;

            retry.Location = new Point(Width / 2 - retry.Width - 30, Height / 2 + 15);
            Controls.Add(retry);

            logoutButton.Location = new Point(Width / 2 + 30, Height / 2 + 15);

            PictureBox back = new PictureBox() {
                BackColor = Color.WhiteSmoke,
                Name = "back",
                Size = new Size(result.Width, result.Height * 8 / 3),
                TabStop = false
            };

            Controls.Add(back);
            back.Location = new Point((Width - back.Width) / 2, (Height - back.Height) / 2);

            Console.WriteLine(result.Width);
            Console.WriteLine(back.Width + " " + back.Height);
            Console.WriteLine(back.Location.X + " " + back.Location.Y);

            GraphicsPath gp = new GraphicsPath();
            Rectangle r = new Rectangle(new Point(0, 0), back.Size);
            int d = 75;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            back.Region = new Region(gp);

            back.BringToFront();
            result.BringToFront();
            retry.BringToFront();
            logoutButton.BringToFront();

            if (showTime) {
                TimeSpan time = DateTime.Now - startTime;
                string timeText = FormatTimeSpan(time); ;
                if (time < map.TimeRecord) {
                    timeText += "\nNUOVO RECORD!";
                    map.TimeRecord = time;
                } else {
                    timeText += "\nRecord: " + FormatTimeSpan(map.TimeRecord);
                }

                Label timings = new Label {
                    AutoSize = true,
                    BackColor = Color.WhiteSmoke,
                    Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point),
                    Size = new Size(10, 10),
                    ForeColor = Color.Black,
                    Name = "resultLabel",
                    TabIndex = 1,
                    Text = timeText,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Controls.Add(timings);

                timings.Location = new Point((Width - timings.Width) / 2, retry.Bottom + 15);
                timings.BringToFront();
            }
        }

        private string FormatTimeSpan(TimeSpan time) {
            return time.Minutes + " min " + time.Seconds + " sec";
        }

    }
}
