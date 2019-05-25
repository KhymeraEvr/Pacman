using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Mechanics
{
    class BreadthSearch
    {
        public GameEngine Game { get; set; }

        private bool[,] visted;
        private Queue<Plate> border = new Queue<Plate>();
        private Dictionary<Plate, Plate> cameFrom = new Dictionary<Plate, Plate>();

        public BreadthSearch(GameEngine game)
        {
            Game = game;
            visted = new bool[Game.Maze1.Width, Game.Maze1.Length];
        }

        private void FloodFill(Plate start)
        {
            border.Enqueue(start);
            cameFrom[start] = null;

            Plate current;
            while (border.Count != 0)
            {
                current = border.Dequeue();
                foreach (Plate dir in current.diretions)
                {
                    if (dir.Passable &&!cameFrom.ContainsKey(dir))
                    {
                        visted[(int)dir.Ypos, (int)dir.Xpos] = true;
                        border.Enqueue(dir);
                        cameFrom[dir] = current;
                    }
                }
            }
        }
        public Queue<Plate> getPath(Plate startPos, Plate finPos)
        {
            Queue<Plate> path = new Queue<Plate>();

            FloodFill(startPos);
            Plate current = Game.Maze1.maze[(int)finPos.Ypos,(int)finPos.Xpos];
            path.Enqueue(current);
            while (current != startPos)
            {                
                current = cameFrom[current];
                path.Enqueue(current);
            }        
            Queue<Plate> reversPath = new Queue<Plate>(path.Reverse());
            reversPath.Dequeue();
            return reversPath;
        }

    }
}
