using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellGame {
    public class GameMap : IEquatable<GameMap> {

        public string Name { get; set; }
        public TimeSpan TimeRecord { get; set; }
        public List<Obstacle> Obstacles { get; } = new List<Obstacle>();
        private int X { get; set; }
        private int Moved { get; set; }

        public GameMap() {
        }

        public GameMap(string name, int startHeight) {
            Name = name;
            AddObstacle(300, 100);
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
            obstacle.Picture.Tag = Data.ObstacleType.END;

            AddObstacle(614, height - obstacle.Height);

            int topLength = 40;
            int tops = X / topLength;
            for (int i = 0; i < tops; i++)
                AddDeadlyObstacle(topLength * i, 0, topLength, 30, false);

            Obstacles.Add(obstacle);
        }

        internal void MoveAll(int length) {
            foreach (var obstacle in Obstacles) {
                obstacle.Picture.Left -= length;
            }

            Moved += length;
        }

        public Obstacle GetLastObstacle() {
            return Obstacles[Obstacles.Count - 1];
        }

        public void Reset() {
            foreach (var obstacle in Obstacles) {
                obstacle.Picture.Left += Moved;
            }
            Moved = 0;
        }

        public override bool Equals(object obj) {
            return Equals(obj as GameMap);
        }

        public bool Equals(GameMap other) {
            return other is not null &&
                   Name == other.Name;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name);
        }

        public static bool operator ==(GameMap left, GameMap right) {
            return EqualityComparer<GameMap>.Default.Equals(left, right);
        }

        public static bool operator !=(GameMap left, GameMap right) {
            return !(left == right);
        }
    }

}
