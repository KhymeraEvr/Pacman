using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman.Strategy;

namespace Pacman
{
   public abstract class Ghost : MovableObject, IEatable
    {
        public Strategy.FollowerStrategy followStrategy;
        public Strategy.ScatterStrategy scatter;
        public Strategy.ScaredStrategy scaredStrategy;
        public Action<string> onReturnToGame;
        public Player Target { get; set; }
        public bool IsScared { get; set; } = false;
        public bool ActivPath { get; set; } = false;
        public IMoveStragegy MoveStrategy { get; set; }
        public int NextTurn { get; set; }
        internal Queue<Plate> Path { get; set; }
        public Plate RespawnPos { get; set; }

        public Ghost() { }
        public Ghost(GameEngine game) : base(game) { }

        public void Respawn()
        {          
            ActivPath = true;
            IsScared = false;
            followStrategy.setTarget(getRespawn);
            followStrategy.getToPosition(RespawnPos);
            MoveStrategy = followStrategy;
            lookAhead();
            MoveSpeed = 0.2f;
        }
       
        public override void Disable()
        {
            IsActive = false;
            CurrentPlate.ObjOnPlate.Remove(this);
        }

        public void Eat()
        {
            if (ActivPath) return;
            Game.GameEvents.Add(new GhostEaten("Ghost", IsScared));
            Respawn();
        }

        public override void lookAhead()
        {            
            MoveStrategy.LookAhead();            
        }

        public abstract void returnToGame();    
        
        Plate getRespawn(int a = 0)
        {
            return RespawnPos;
        }

        public void FollowPath()
        {
            if (Path.Count == 0) followStrategy.Refresh();
            nextPlate = Path.Dequeue();
            for (int i = 0; i < CurrentPlate.diretions.Length; i++)
            {
                if (CurrentPlate.diretions[i] == nextPlate)
                {
                    Direction = (Direction)i;
                }
            }
        }

        public void getToPosition(Plate destination)
        {
            Mechanics.BreadthSearch aStar = new Mechanics.BreadthSearch(Game);
            Path = aStar.getPath(CurrentPlate, destination);
            
        }

        public override void Spawn(int x, int y)
        {
            Xpos = x;
            Ypos = y;
            ActivPath = false;
            IsScared = false;
            Radius = 1f;
            CurrentPlate = Game.Maze1.maze[y, x];
            nextPlate = CurrentPlate.diretions[3];
            Direction = Direction.RIGHT;
            NextDirection = Direction.RIGHT;
            Game.Maze1.maze[y, x].ObjOnPlate.Add(this);
            IsActive = true;
            setStrategies();
        }

        public virtual void findPlate(int direction)
        {
            if (!IsScared || (IsScared && direction != (int)Game.FindTargetDirection(Target, this)))
            {
                if (CurrentPlate.diretions[direction].Passable)
                {
                    nextPlate = CurrentPlate.diretions[direction];
                    return;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (i != (direction + 2) % 4 && CurrentPlate.diretions[i].Passable)
                {
                    nextPlate = CurrentPlate.diretions[i];
                    Direction = (Direction)i;
                    return;
                }
            }
        }

        public override void startMoving()
        {
            Move();
            if (Math.Abs(nextPlate.Xpos - Xpos) < 0.03f && Math.Abs(nextPlate.Ypos - Ypos) < 0.03f)
            {
                CurrentPlate.ObjOnPlate.Remove(this);
                CurrentPlate = nextPlate;
                CurrentPlate.ObjOnPlate.Add(this);
                Xpos = CurrentPlate.Xpos;
                Ypos = CurrentPlate.Ypos;
                lookAhead();
            }
            if (Math.Abs(CurrentPlate.Xpos - Xpos) > 1 || Math.Abs(CurrentPlate.Ypos - Ypos) > 1)
            {
                setPlatePosition();
            }
        }
        public virtual void setPlatePosition()
        {
            Xpos = CurrentPlate.Xpos;
            Ypos = CurrentPlate.Ypos;
        }
        public abstract void setStrategies();
    }
}
