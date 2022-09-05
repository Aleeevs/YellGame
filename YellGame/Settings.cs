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
    public partial class Settings : Form {

        public static double Sensibility;
        public static int Device;

        public static void OpenAndExecuteOnClose(Action<object, EventArgs> action) {
            Settings settings = new Settings();
            settings.FormClosed += (s, e) => {
                if (action != null)
                    action.Invoke(s, e);
            };
            settings.ShowDialog();
        }

        public Settings() {
            InitializeComponent();
            ScanSoundCards();

            this.cbDevice.SelectedIndex = Device;
            this.sensibilityBar.Value = (int) (Sensibility * 2);
        }

        private void ScanSoundCards() {
            cbDevice.Items.Clear();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
                cbDevice.Items.Add(WaveIn.GetCapabilities(i).ProductName);

            if (cbDevice.Items.Count == 0)
                MessageBox.Show("Non sono presenti dispositivi di ingresso.");
            else
                cbDevice.SelectedIndex = 0;
        }

        private void sensibilityBar_Scroll(object sender, EventArgs e) {
            Sensibility = sensibilityBar.Value / 2d;
            Text = Sensibility + " ";
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e) {
            Device = cbDevice.SelectedIndex;
        }
    }
}
