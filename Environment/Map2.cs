using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Environment
{
    public class Map2 : Map
    {
        public Map2() : base(7, 2)
        {
           
            using (StreamReader sw = new StreamReader("Map2.txt"))
            {
                MapJSON = sw.ReadLine();
            }
            this.maze = JsonConvert.DeserializeObject<int[,]>(MapJSON);
            maze[14, 0] = 1;
            maze[14, 27] = 1;
            MapJSON = '{' + MapJSON + '}';

        }
    }
}
