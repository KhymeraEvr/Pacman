using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Mechanics
{
    public class PlayerEaten : FoodEaten
    {
        public GameEngine Game { get; set; }
        public static List<Tuple<string, Action>> Actions { get; set; } = new List<Tuple<string, Action>>();

        public PlayerEaten(string type,GameEngine game) : base(type, null)
        {
            Game = game;
        }

        override public void Execute(string id)
        {
            
            Game.Maze1.gameStarted = false;
            foreach (var action in Actions)
            {
                if(action.Item1 == id) action.Item2.Invoke();
            }


        }

    }
}
