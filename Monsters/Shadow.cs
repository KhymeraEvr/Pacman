using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Shadow : Ghost
    {
        readonly Plate patrolCorner;

        public Shadow(GameEngine game):base(game)
        {
            patrolCorner = game.Maze1.maze[2, 26];
            RespawnPos = game.Maze1.maze[game.Maze1.Width / 2 - 2, game.Maze1.Length / 2]; ;
        }

        public override void returnToGame()
        {
            ActivPath = false;
            IsScared = false;
            Target = Game.Player;
            followStrategy.setTarget(Game.findFutureTargetPlate);
            followStrategy.getToPosition(Game.Player.CurrentPlate);
            NextTurn = 2;

            MoveSpeed = 0.05f;
            MoveStrategy = followStrategy;
            lookAhead();
            if (onReturnToGame != null) onReturnToGame.Invoke("shadow");
        }
        public override void setStrategies()
        {
            scatter = new Strategy.ScatterStrategy(this, (int)patrolCorner.Xpos, (int)patrolCorner.Ypos);
            scaredStrategy = new Strategy.ScaredStrategy(this);
            followStrategy = new Strategy.FollowerStrategy(Game.findFutureTargetPlate, this, 0);
            NextTurn = 2;
            MoveStrategy = followStrategy;
        }

        

        public override void lookAhead()
        {           
            MoveStrategy.LookAhead();
        }       

        override public void findPlate(int direction)
        {

            Direction playerDir = Game.FindTargetDirection(Target,this);
            if (IsScared) playerDir = (Direction)((int)(playerDir + 2)% 4);
            if (CurrentPlate.diretions[(int)playerDir].Passable)
            {
                nextPlate = CurrentPlate.diretions[(int)playerDir];
                Direction = playerDir;
            }
            else if (CurrentPlate.diretions[direction].Passable)
            {
                nextPlate = CurrentPlate.diretions[direction];
                return;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != direction && CurrentPlate.diretions[i].Passable)
                    {
                        if (IsScared && i == (int)Game.FindTargetDirection(Target,this)) continue;
                        nextPlate = CurrentPlate.diretions[i];
                        Direction = (Direction)i;
                    }
                }
            }
        }

    }
}
