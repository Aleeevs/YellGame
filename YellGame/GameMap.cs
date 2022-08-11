using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellGame {
    class GameMap {

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

        public GameMap AddObstacle(int width, int height) {
            Obstacles.Add(new Obstacle(X, 504 - height, width, height));
            X += width;
            return this;
        }

        public void SetEnd(int height) {
            Obstacle obstacle = new Obstacle(X, 504 - height, 614, 29);
            var picture = obstacle.Picture;

            picture.Image = Properties.Resources.finish;

            AddObstacle(614, height - obstacle.Height);
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
