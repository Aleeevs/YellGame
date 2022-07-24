using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YellGame {
    class Obstacle {

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
                Tag = "obstacle"
            };
        }

        public PictureBox Picture { get; set; }
        public bool Locked { get; set; }
        public int Left { get => Picture.Left; }
        public int Top { get => Picture.Top; }
        public int Right { get => Picture.Right; }
        public int Bottom { get => Picture.Bottom; }
        public int Width { get => Picture.Width; }
        public int Height { get => Picture.Height; }

        /*
        public bool HasIn(PictureBox other) {
            return HasIn(other.Left, other.Right, other.Top, other.Bottom);
        }

        public bool HasIn(int left, int top) {
            return left > Left && left < Right &&
                top > Top && top < Bottom;
        }
        */

        public bool HasIn(int x, int y, int width, int height) {
            return new Rectangle(x, y, width, height).IntersectsWith(new Rectangle(Picture.Location, Picture.Size));
        }

        public bool HasOn(int x, int y, int width) {
            return new Rectangle(x, y, width, 1).IntersectsWith(new Rectangle(Left, Top, Width, 1));
        }

        public ObstacleSide? IntersectOn(int x, int y, int width, int height, int m) {
            if (new Rectangle(x, y + height, width, m).IntersectsWith(new Rectangle(Left, Top, Width, m)))
                return ObstacleSide.TOP;
            if (new Rectangle(x, y, width, m + 1).IntersectsWith(new Rectangle(Left, Bottom, Width, m + 1)))
                return ObstacleSide.BOTTOM;
            if (new Rectangle(x + width, y, m, height).IntersectsWith(new Rectangle(Left, Top, m, Height)))
                return ObstacleSide.LEFT;
            if (new Rectangle(x, y, m, height).IntersectsWith(new Rectangle(Right, Top, m, Height)))
                return ObstacleSide.RIGHT;

            return null;
        }

    }

    enum ObstacleSide {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }
}
