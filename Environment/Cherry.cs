using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Cherry : GameObject, IEatable
    {
        Plate currentPlate;

        public Cherry(GameEngine game)
        {
            Game = game;
        }

        public GameEngine Game { get; set; }
        public override void Disable()
        {
            currentPlate.ObjOnPlate.Remove(this);
            IsActive = false;
        }

        public void Eat()
        {
            Game.GameEvents.Add(new CherryEaten("Cherry", this));
            Disable();
        }

        public override void Spawn(int x, int y)
        {
            Xpos = y;
            Ypos = x;
            Radius = 0.1f;
            Game.Maze1.maze[x, y].ObjOnPlate.Add(this);
            currentPlate = Game.Maze1.maze[x, y];
            IsActive = true;
        }
    }
}
