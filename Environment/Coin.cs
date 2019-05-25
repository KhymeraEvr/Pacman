using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Coin : GameObject, IEatable
    {
        int scoreValue = 1;
        Plate currentPlate;
        public GameEngine Game { get; set; }
        public int ScoreValue { get => scoreValue; set => scoreValue = value; }

        public override void Disable()
        {
            currentPlate.ObjOnPlate.Remove(this);
            IsActive = false;
        }

        public Coin() { }
        public Coin(int x, int y, GameEngine game)
        {
            Game = game;
            Spawn(x, y);
        }

        public void Eat()
        {
            Game.GameEvents.Add(new FoodEaten("Coin", this));
            if (--Game.Maze1.CoinsCount <= 0)
            {
                Game.GameEvents.Add(new Mechanics.GameOverEvent());
            }
            Game.Maze1.coins[(int)Ypos, (int)Xpos] = false;
            Disable();

        }

        public override void Spawn(int x, int y)
        {
            Xpos = y;
            Ypos = x;
            Radius = 0.1f;           
            currentPlate = Game.Maze1.maze[x, y];
            IsActive = true;
        }
    }
}
