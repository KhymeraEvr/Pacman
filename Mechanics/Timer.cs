using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Pacman.Mechanics
{
    public class GameTimer
    {
        public List< Action> ScareOverActions = new List<Action>();

        public GameEngine Game { get; set; }
        public Timer timer = new Timer(10000);
        bool scaredMode = false;
        public bool ChaseMode { get; set; } = true;

        public void Start()
        {
            timer.Elapsed += gameModeChange;
            timer.AutoReset = true;
            timer.Enabled = true;          
            CherryEaten.Actions.Add(new Tuple<string,Action>(Game.GameId,SetScaredTime));
        }

        void gameModeChange(object source, ElapsedEventArgs a)
        {
            ChaseMode = !ChaseMode;
            if (scaredMode)
            {
                ChaseMode = true;
                scaredMode = false;
                foreach(Action acr in ScareOverActions)
                {
                    acr.Invoke();
                }
            }
            if (ChaseMode) timer.Interval = 10000;
            else timer.Interval = 5500;

            foreach (Ghost gh in Game.GhostsCol)
            {
                if (gh.ActivPath) continue;
                if (ChaseMode)
                {
                    gh.MoveStrategy = gh.followStrategy;                    
                }
                else
                {
                    gh.scatter.needRefresh = true;
                    gh.MoveStrategy = gh.scatter;
                }
            }
        }

        public void SetScaredTime()
        {
            scaredMode = true;
            timer.Interval = 8000;
        }
    }
}
