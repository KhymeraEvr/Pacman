using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Monsters
{
    class Bashfull : Ghost
    {
        readonly Plate patrolCorner;
        

        public Bashfull(GameEngine game) : base(game)
        {
            patrolCorner = Game.Maze1.maze[1, 1];
            RespawnPos = Game.Maze1.maze[Game.Maze1.Width/2, Game.Maze1.Length/2 - 1];
        }
        public override void setStrategies()
        {
            followStrategy = new Strategy.FollowerStrategy(FindPlate, this, 0);
            scatter = new Strategy.ScatterStrategy(this, (int)patrolCorner.Xpos, (int)patrolCorner.Ypos);
            scaredStrategy = new Strategy.ScaredStrategy(this);
            NextTurn = 3;
            MoveStrategy = followStrategy;
        }

        public override void returnToGame()
        {
            ActivPath = false;
            IsScared = false;
            Target = Game.Player;
            followStrategy.setTarget(FindPlate);
            followStrategy.getToPosition(FindPlate());
            NextTurn = 3;

            MoveSpeed = 0.05f;
            MoveStrategy = followStrategy;
            lookAhead();
            if (onReturnToGame != null) onReturnToGame.Invoke("bashfull");
        }

        Plate FindPlate(int a = 0)
        {
            double shadown_playerXDiff = Math.Abs(Game.Player.CurrentPlate.Xpos - Game.GhostsCol[0].CurrentPlate.Xpos);
            double shadown_playerYDiff = Math.Abs(Game.Player.CurrentPlate.Ypos - Game.GhostsCol[0].CurrentPlate.Ypos);
            int shadow_playerDist =(int) Math.Sqrt(shadown_playerXDiff * shadown_playerXDiff + shadown_playerYDiff * shadown_playerYDiff);
            Plate res = Game.findFutureTargetPlate(shadow_playerDist * 2);
            return res;
        }
    }
}
