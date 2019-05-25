using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
   public abstract class GameEvent
    {

        public abstract void Execute(string id);
    }

    public class FoodEaten : GameEvent
    {
        public static List<Tuple<string, Action<int, int>>> Actions { get; set; } = new List<Tuple<string, Action<int, int>>>();
        public string FoodType { get; set; }
        public GameObject Pos { get; set; }

        public FoodEaten() { }

        public FoodEaten(string type, GameObject pos)
        {
            FoodType = type;
            Pos = pos;
        }

        public override void Execute(string id)
        {
            if (Pos == null) return;
            foreach(var plsHelp in Actions)
            {
                if(plsHelp.Item1 == id) plsHelp.Item2.Invoke((int)Pos.Xpos,(int)Pos.Ypos);
            }
        }
    }

    public class CherryEaten : FoodEaten
    {
        public static List<Tuple<string, Action>> Actions { get; set; } = new List<Tuple<string, Action>>();

        public CherryEaten(string type, GameObject pos) : base(type, pos)
        {          
        }

        override public void Execute(string id)
        {
            foreach (var act in Actions)
            {
                if(act.Item1 == id)
                {
                    act.Item2.Invoke();
                }
            }
        }
    }


    public class GhostEaten : FoodEaten
    {
        public bool GhostWasScared { get; set; }
        new public static List<Action> Actions { get; set; } = new List<Action>();

        public GhostEaten(string type, bool _ghostWasScared) : base(type, null)
        {
            GhostWasScared = _ghostWasScared;
        }
    }
}
