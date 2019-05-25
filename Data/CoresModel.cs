using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Data
{
    public class CoresModel
    {
        public List<ObjectCores> cores;

        public CoresModel(GameEngine game)
        {
            Game = game;
        }

        public GameEngine Game { get; set; }
        private string DirToString(Direction dir)
        {
            switch (dir)
            {
                case Direction.UP:
                    return "UP";
                case Direction.RIGHT:
                    return "RIGHT";
                case Direction.DOWN:
                    return "DOWN";
                case Direction.LEFT:
                    return "LEFT";
                default:
                    return "";
            }
        }

        public void Update()
        {
            if (cores == null)
            {
                cores = new List<ObjectCores>
            {
                new PacmanCores(){Name = "pacman",X = (float)Game.Player.Xpos, Y = (float)Game.Player.Ypos, Direction = DirToString(Game.Player.Direction)},
                new ObjectCores(){Name = "shadow",X = (float)Game.GhostsCol.Ghosts[0].Xpos, Y = (float)Game.GhostsCol.Ghosts[0].Ypos},
                new ObjectCores(){Name = "speedy",X = (float)Game.GhostsCol.Ghosts[1].Xpos, Y = (float)Game.GhostsCol.Ghosts[1].Ypos},
                new ObjectCores(){Name = "bashfull",X = (float)Game.GhostsCol.Ghosts[2].Xpos, Y = (float)Game.GhostsCol.Ghosts[2].Ypos},
                new ObjectCores(){Name = "pokey",X = (float)Game.GhostsCol.Ghosts[3].Xpos, Y = (float)Game.GhostsCol.Ghosts[3].Ypos}
        };
            }
            else
            {
                cores[0].X = (float)Game.Player.Xpos;
                cores[0].Y = (float)Game.Player.Ypos;
                ((PacmanCores)cores[0]).Direction = DirToString(Game.Player.Direction);
                for (int i = 1; i < cores.Count; i++)
                {
                    cores[i].X = (float)Game.GhostsCol[i - 1].Xpos;
                    cores[i].Y = (float)Game.GhostsCol[i - 1].Ypos;
                }
            }

        }

    }
}
