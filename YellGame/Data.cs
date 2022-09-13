using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace YellGame {
    class Data {

        public static readonly string TimeRecordsPath = "..\\..\\..\\records.csv";

        public static double Sensibility;
        public static int Device;

        public static HashSet<GameMap> maps = new HashSet<GameMap>();

        public static void LoadMaps(string path) {
            string[] files = Directory.GetFiles(path);

            foreach (string filePath in files) {
                FileInfo file = new FileInfo(filePath);
                GameMap map = null;
                String name = file.Name.Replace(".csv", "");

                using StreamReader reader = new StreamReader(file.FullName);
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string[] arr = line.Split(":");

                    if (arr.Length < 2) continue;

                    int height = Convert.ToInt32(arr[1]);

                    if (map == null && arr[0] == ObstacleType.START.ToString()) {
                        map = new GameMap(name, height);
                        continue;
                    }

                    if (map == null) break;

                    int width = height;
                    bool end = false;

                    switch (Enum.Parse(typeof(ObstacleType), arr[0])) {
                        case ObstacleType.OBSTACLE:
                            if (arr.Length < 3) continue;
                            height = Convert.ToInt32(arr[2]);
                            map.AddObstacle(width, height);
                            break;
                        case ObstacleType.VOID:
                            map.AddVoid(width);
                            break;
                        case ObstacleType.END:
                            map.SetEnd(width);
                            end = true;
                            break;
                    }

                    if (end) break;
                }

                if (map == null || (ObstacleType)map.GetLastObstacle().Picture.Tag != ObstacleType.END) {
                    Console.WriteLine("Errore nel caricare la mappa " + name);
                    continue;
                }

                maps.Add(map);
            }
         
        }

        public static void LoadTimeRecords() {
            using StreamReader reader = new StreamReader(TimeRecordsPath);
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] arr = line.Split("/");
                string name = arr[0];
                TimeSpan time = TimeSpan.FromSeconds(Convert.ToDouble(arr[1]));

                if (maps.TryGetValue(new GameMap() { Name = name }, out GameMap map))
                    map.TimeRecord = time;
            }
        }

        public static void SaveTimeRecords() {
            using StreamWriter writer = new StreamWriter(TimeRecordsPath);
            foreach (GameMap map in maps) {
                writer.WriteLine(map.Name + "/" + map.TimeRecord.TotalSeconds);
            }
        }

        public enum ObstacleType {
            START,
            OBSTACLE,
            VOID,
            END
        }

    }
}
