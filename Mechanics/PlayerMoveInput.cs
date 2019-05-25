using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pacman.Mechanics
{
    public class PlayerMoveInput
    {        
        public bool Parsing = true;
        public Player player;

        public PlayerMoveInput(Player player)
        {
            this.player = player;
        }

        void tryPass(Player pl, ref bool started)
        {
            while (true)
            {
                if (Keyboard.IsKeyDown(Key.Up))
                {
                    pl.NextDirection = Direction.UP;
                    if (started == false)
                    {
                        pl.Direction = Direction.UP;
                        started = true;
                    }
                }
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    pl.NextDirection = Direction.DOWN;
                    if (started == false)
                    {
                        pl.Direction = Direction.DOWN;
                        started = true;
                    }
                }
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    pl.NextDirection = Direction.LEFT;
                    if (started == false)
                    {
                        pl.Direction = Direction.LEFT;
                        started = true;
                    }
                }
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    pl.NextDirection = Direction.RIGHT;
                    if (started == false)
                    {
                        pl.Direction = Direction.RIGHT;
                        started = true;
                    }
                }
            }
        }
        public void tryPassDirection(string CurrentInput)
        {
            bool gameStarted = player.Game.Maze1.gameStarted;
            switch (CurrentInput)
            {
                case "UP":
                    player.NextDirection = Direction.UP;
                    if (!gameStarted)
                    {
                        player.Direction = Direction.UP;
                        player.Game.Maze1.gameStarted = true;
                    }
                    break;
                case "DOWN":
                    player.NextDirection = Direction.DOWN;
                    if (!gameStarted)
                    {
                        player.Direction = Direction.DOWN;
                        player.Game.Maze1.gameStarted = true;
                    }
                    break;
                case "LEFT":
                    player.NextDirection = Direction.LEFT;
                    if (!gameStarted)
                    {
                        player.Direction = Direction.LEFT;
                        player.Game.Maze1.gameStarted = true;
                    }
                    break;
                case "RIGHT":
                    player.NextDirection = Direction.RIGHT;
                    if (!gameStarted)
                    {
                        player.Direction = Direction.RIGHT;
                        player.Game.Maze1.gameStarted = true;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
