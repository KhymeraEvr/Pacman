using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Monsters
{
    class Pokey : Ghost
    {
        readonly Plate patrolCorner;
        Plate currentChasePlate;


        public Pokey(GameEngine game) : base(game)
        {
            patrolCorner = game.Maze1.maze[29, 1];
            RespawnPos = game.Maze1.maze[game.Maze1.Width / 2, game.Maze1.Length / 2];
        }

        public override void setStrategies()
        {
            currentChasePlate = Game.Player.CurrentPlate;
            scatter = new Strategy.ScatterStrategy(this, (int)patrolCorner.Xpos, (int)patrolCorner.Ypos);
            scaredStrategy = new Strategy.ScaredStrategy(this);
            followStrategy = new Strategy.FollowerStrategy(getTargetPlate, this, 0);
            NextTurn = 3;
            MoveStrategy = followStrategy;
        }

        public override void returnToGame()
        {
            IsScared = false;
            ActivPath = false;
            Target = Game.Player;
            followStrategy.setTarget(getTargetPlate);
            followStrategy.getToPosition(getTargetPlate());
            NextTurn = 2;
            MoveSpeed = 0.05f;
            MoveStrategy = followStrategy;
            lookAhead();
            if (onReturnToGame != null) onReturnToGame.Invoke("pokey");
        }

        public Plate getTargetPlate(int a = 0)
        {
            if (Game.getDistance(Xpos, currentChasePlate.Xpos, Ypos, currentChasePlate.Ypos) > 8)
            {
                return currentChasePlate;
            }
            else
            {
                currentChasePlate = Game.Player.CurrentPlate;
                return patrolCorner;
            }
        }
    }
}
