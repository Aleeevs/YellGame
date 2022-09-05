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

namespace YellGame {
    public partial class GameForm : Form {

        bool stop = false;
        int up = 0;
        int right = 0;

        GameMap map;
        WaveInEvent waveIn;

        public GameForm(GameMap map) {
            InitializeComponent();
            this.map = map;
            Text = "YellGame | " + map.Name;
        }

        private void timer_Tick(object sender, EventArgs e) {
            if (stop) return;

            // Controlla se ci sono dispositivi input collegati, altrimenti ferma il gioco
            if (WaveIn.DeviceCount == 0) {
                stop = true;
                MessageBox.Show("Nessun dispositivo di input audio trovato.");
                Settings.OpenAndExecuteOnCloseExecute((s, e) => stop = false);
                return;
            }

            // Imposta il dispositivo di input selezionato
            waveIn.DeviceNumber = Settings.Device;

            int horizontal = 0; // movimento orizzontale
            int vertical = 0; // movimento verticale
            bool moveAll = false; 

            // Aumenta il movimento orizzontale se può spostarsi verso destra
            if (this.right != 0 && player.Right < 780) {
                horizontal = this.right;

                // Se il giocatore ha raggiunto i 7/10 dello schermo, allora tutti gli ostacoli dovranno scorrere
                // Effetto super mario
                moveAll = player.Right >= Width * 7 / 10 && map.GetLastObstacle().Right != Width;
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
                stop = true;
                this.winLabel.Visible = true;
                return;
            }

            // Se si deve spostare la mappa a sinistra (super mario), il giocatore non si sposta
            if (moveAll)
                horizontal = 0;

            // Spostamento della posizione del giocatore
            player.Left += horizontal;
            player.Top += vertical;

            // Effetto super mario
            if (moveAll)
                map.MoveAll();

            // Se il giocatore è finito nel fosso o ha toccato un ostacolo deadly, ha perso
            if (player.Bottom >= 510 || dead) {
                stop = true;
                loseLabel.Visible = true;
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
            maxValue += (int) (Settings.Sensibility / 100 * maxValue);
            float volume = (100 * peakValue) / maxValue;

            up = (int) volume * 10 / 100; // determina il movimento verticale
            right = (int) volume * 14 / 100; // determina il movimento orizzontale
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            // Apro le impostazioni. Quando vengono chiuse, il gioco riprende
            stop = true;
            Settings.OpenAndExecuteOnCloseExecute((s, e) => stop = false);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
