using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Environment
{
    public class Map1 : Map
    {
        public Map1() : base(14,1)
        {
           
            using (StreamReader sw = new StreamReader("Map1.txt"))
            {
                MapJSON = sw.ReadLine();
            }
            this.maze = JsonConvert.DeserializeObject<int[,]>(MapJSON);
            MapJSON = '{' + MapJSON + '}';
        }
    }
}
