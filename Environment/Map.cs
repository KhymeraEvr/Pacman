using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Environment
{
    public class Map
    {
        public readonly int TunnelY;
        public readonly int id;
        public string MapJSON;
        public int[,] maze;
        protected Map(int tunnelY, int Id)
        {
            TunnelY = tunnelY;
            id = Id;
        }
    }
}
