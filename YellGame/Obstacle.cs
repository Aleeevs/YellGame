using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YellGame {
    public class Obstacle {

        public Obstacle(PictureBox picture) {
            Picture = picture;
        }

        public Obstacle(int x, int y, int width, int height) {
            Picture = new PictureBox() {
                BackColor = Color.Black,
                Location = new Point(x, y),
                Name = "obstacle" + x + "-" + y,
                Size = new Size(width, height),
                TabIndex = 1,
                TabStop = false,
                Tag = Data.ObstacleType.OBSTACLE
            };
        }


        public PictureBox Picture { get; set; }
        public bool Deadly { get; set; }
        public int Left { get => Picture.Left; }
        public int Top { get => Picture.Top; }
        public int Right { get => Picture.Right; }
        public int Bottom { get => Picture.Bottom; }
        public int Width { get => Picture.Width; }
        public int Height { get => Picture.Height; }

    }
}
