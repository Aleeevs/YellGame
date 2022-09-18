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
    public partial class MapSelectionForm : Form {

        public MapSelectionForm() {
            InitializeComponent();
            for (int i = 0; i < Data.Maps.Count; i++)
                if (i == 0 || Data.Maps[i - 1].TimeRecord.TotalSeconds != -1)
                    levelBox.Items.Add(Data.Maps[i].Name);

            levelBox.Font = new Font(Data.CustomFonts.Families[0], 12);
        }

        private void levelBox_SelectedIndexChanged(object sender, EventArgs e) {
            int index = levelBox.SelectedIndex;
            GameMap map = Data.Maps[index];
            Close();
            MainMenu.Instance.Hide();
            new GameForm(map).Show();
        }
    }
}
