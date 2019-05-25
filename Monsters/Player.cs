using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Player : MovableObject, IEatable
    {
        public Plate respawnPos;
        public int LivesRemain { get; set; } = 3;    
        bool stopped = false;


        public Plate RespawnPos => respawnPos;


        public Player(GameEngine game) : base(game)
        {
            Game.PlayerInput = new Mechanics.PlayerMoveInput(this);
            respawnPos = Game.Maze1.maze[Game.Maze1.Width / 2 + 2, Game.Maze1.Length / 2];
        }

        public override void lookAhead()
        {
            if (CurrentPlate.diretions[(int)NextDirection].Passable)
            {
                nextPlate = CurrentPlate.diretions[(int)NextDirection];
                Direction = NextDirection;
                stopped = false;
            }
            else if (CurrentPlate.diretions[(int)Direction].Passable)
            {
                nextPlate = CurrentPlate.diretions[(int)Direction];
                stopped = false;
            }
            else stopped = true;
        }

        public override void startMoving()
        {
            checkForFood();

            if (!stopped) Move();
            if (Math.Abs(nextPlate.Xpos - Xpos) < 0.03f && Math.Abs(nextPlate.Ypos - Ypos) < 0.03f)
            {
                CurrentPlate.ObjOnPlate.Remove(this);
                CurrentPlate = nextPlate;
                CurrentPlate.ObjOnPlate.Add(this);
                Xpos = CurrentPlate.Xpos;
                Ypos = CurrentPlate.Ypos;
                lookAhead();

            }

        }

        void checkForFood()
        {
            if (CurrentPlate.ObjOnPlate.Count > 1)
            {
                for (int i = 0; i < CurrentPlate.ObjOnPlate.Count; i++)
                {
                    if (CurrentPlate.ObjOnPlate[i] != this && Game.collisonDetect(this, CurrentPlate.ObjOnPlate[i]))
                    {
                        if (CurrentPlate.ObjOnPlate[i] is Ghost && ((Ghost)CurrentPlate.ObjOnPlate[i]).ActivPath) continue;
                        if (CurrentPlate.ObjOnPlate[i] is Ghost && !((Ghost)CurrentPlate.ObjOnPlate[i]).IsScared) Eat();
                        else ((IEatable)CurrentPlate.ObjOnPlate[i]).Eat();
                    }
                }
            }
        }


        public override void Spawn(int x, int y)
        {
            Xpos = x;
            Ypos = y;
            Radius = 1f;
            CurrentPlate = nextPlate = Game.Maze1.maze[y, x];
            Game.Maze1.maze[y, x].ObjOnPlate.Add(this);
            Direction = NextDirection = Direction.UP;
            IsActive = true;
            lookAhead();
        }

        public override void Disable()
        {
            IsActive = false;
        }

        public void Eat()
        {
            LivesRemain--;
            Game.GameEvents.Add(new Mechanics.PlayerEaten("Player", Game));
            if (LivesRemain < 1) Game.GameEvents.Add(new Mechanics.GameOverEvent());
            IsActive = false;
            CurrentPlate.ObjOnPlate.Remove(this);
            Spawn((int)respawnPos.Xpos, (int)respawnPos.Ypos);
        }
    }
}
