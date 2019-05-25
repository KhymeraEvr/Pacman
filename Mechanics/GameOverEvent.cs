using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Mechanics
{
    public class GameOverEvent : GameEvent
    {
        public static List<Tuple<string, Action>> Actions { get; set; } = new List<Tuple<string, Action>>();

        public override void Execute(string id)
        {
            foreach (var act in Actions)
            {
                if (act.Item1 == id) act.Item2.Invoke();
            }
            Console.WriteLine("gameOver");
        }
    }
}
