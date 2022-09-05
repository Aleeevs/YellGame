using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellGame {
    public class GameMap {

        public string Name { get; }
        public List<Obstacle> Obstacles { get; } = new List<Obstacle>();
        private int X { get; set; }

        public GameMap(string name) {
            Name = name;
        }

        public GameMap AddVoid(int width) {
            X += width;
            return this;
        }

        public Obstacle CreateRelativeObstacle(int width, int height) {
            return new Obstacle(X, 504 - height, width, height);
        }

        public Obstacle CreateObstacle(int left, int top, int width, int height) {
            return new Obstacle(left, top, width, height);
        }

        public GameMap AddObstacle(int width, int height) {
            Obstacles.Add(CreateRelativeObstacle(width, height));
            X += width;
            return this;
        }

        public GameMap AddDeadlyObstacle(int left, int top, int width, int height, bool up = true) {
            Obstacle obstacle = CreateObstacle(left, top, width, height);
            obstacle.Deadly = true;
            obstacle.Picture.Image = up ? Properties.Resources.triangle : Properties.Resources.triangledown;
            obstacle.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            Obstacles.Add(obstacle);
            return this;
        }

        public void SetEnd(int height) {
            Obstacle obstacle = new Obstacle(X, 504 - height, 614, 29);
            obstacle.Picture.Image = Properties.Resources.finish;

            AddObstacle(614, height - obstacle.Height);

            int topLength = 40;
            int tops = X / topLength;
            for (int i = 0; i < tops; i++)
                AddDeadlyObstacle(topLength * i, 0, topLength, 30, false);

            Obstacles.Add(obstacle);
        }

        internal void MoveAll() {
            foreach (var obstacle in Obstacles) {
                obstacle.Picture.Left -= 2;
            }
        }

        public Obstacle GetLastObstacle() {
            return Obstacles[Obstacles.Count - 1];
        }

    }

}
