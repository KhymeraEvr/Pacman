using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Environment
{
    public class Map3 : Map
    {
        public Map3() : base(23, 3)
        {           
            using (StreamReader sw = new StreamReader("Map3.txt"))
            {
                MapJSON = sw.ReadLine();
            }
            this.maze = JsonConvert.DeserializeObject<int[,]>(MapJSON);
            MapJSON = '{' + MapJSON + '}';

        }
    }
}
