using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pacman.Mechanics;

namespace Pacman
{
    public class GameEngine : IDisposable
    {
        private int eventSize = 0;
        public Data.CoresModel coresModel;
        public string GameId { get; set; }
        public int Score { get; set; }
        public List<GameEvent> GameEvents { get; set; } = new List<GameEvent>();
        public GhostCollcection GhostsCol { get; set; }
        public Player Player { get; set; }
        public Maze Maze1 { get; set; }
        public PlayerMoveInput PlayerInput { get; set; }
        public Environment.Map Map { get; set; }
        public string mapJSON { get; set; }
        public string PlayerName { get; set; }

        public GameEngine()
        {
            Console.WriteLine("GM created");
        }

        public  void startGame()
        {
            GhostsCol.ConnectionId = GameId;
            coresModel = new Data.CoresModel(this);
            Maze1.FrameRate = 20;
            PlayerInput = new Mechanics.PlayerMoveInput(Player);
            RunMaze();
        }

        public void RunMaze()
        {
            Task.Run(new Action(Maze1.runMaze));
        }

        public void MapInit(int id)
        {
            switch (id)
            {
                case 1:
                    Map = new Environment.Map1();
                    break;
                case 2:
                    Map = new Environment.Map2();
                    break;
                case 3:
                    Map = new Environment.Map3();
                    break;
                default:
                    break;
            }

        }

        public  void eventsCheck()
        {
            if (GameEvents.Count > eventSize)
            {
                FoodEaten curEvent;
                for (int i = 0; i < GameEvents.Count; i++)
                {
                    curEvent = GameEvents[i] as FoodEaten;
                    if (curEvent != null)
                    {
                        switch (curEvent.FoodType)
                        {
                            case "Coin":
                                Score += 10;
                                break;
                            case "Cherry":
                                Score += 100;
                                break;
                            case "Ghost":
                                Score += 200;
                                break;
                            case "Player":
                                Mechanics.PlayerEaten ev = curEvent as Mechanics.PlayerEaten;
                                break;
                        }
                        if(curEvent.FoodType != "Ghost")
                        {
                            curEvent.Execute(GameId);
                        }
                        if (GameEvents[GameEvents.Count - 1] is Mechanics.GameOverEvent)
                        {
                            GameEvents[GameEvents.Count - 1].Execute(GameId);
                        }
                        GameEvents.Remove(curEvent);

                    }
                }
            }
            else eventSize = GameEvents.Count;
        }

        public  bool collisonDetect(GameObject a, GameObject b)
        {
            double lBoundA = a.Xpos - a.Radius;
            double rBoundA = a.Xpos + a.Radius;
            double lBoundB = b.Xpos - b.Radius;
            double rBoundB = b.Xpos + b.Radius;
            if (lBoundA < rBoundB && rBoundA > lBoundB)
            {
                lBoundA = a.Ypos - a.Radius;
                rBoundA = a.Ypos + a.Radius;
                lBoundB = b.Ypos - b.Radius;
                rBoundB = b.Ypos + b.Radius;
                if (lBoundA < rBoundB && rBoundA > lBoundB)
                {
                    return true;
                }
            }
            return false;
        }


        public  Plate findFutureTargetPlate(int stepsAhead)
        {
            Plate resPlate = Player.CurrentPlate;
            int numberToLookAhead = stepsAhead;
            Direction resDirection = Player.Direction;

            for (int i = 0; i < numberToLookAhead; i++)
            {
                if (resPlate.Xpos == 0 || resPlate.Xpos == 27) return Maze1.maze[13, 13];
                if (resPlate.diretions[(int)Player.NextDirection].Passable)
                {
                    resPlate = resPlate.diretions[(int)Player.NextDirection];
                    resDirection = Player.NextDirection;

                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (resPlate.diretions[j] != null && resPlate.diretions[j].Passable && j != ((int)resDirection + 2) % 4)
                        {
                            resPlate = resPlate.diretions[j];
                            resDirection = (Direction)j;
                        }
                    }
                }

            }
            return resPlate;


        }

         public Direction FindTargetDirection(GameObject target, GameObject caller)
        {

            if (Math.Abs((int)(target.Xpos - caller.Xpos)) > Math.Abs((int)(target.Ypos - caller.Ypos)))
            {
                if ((target.Xpos - caller.Xpos) > 0)
                {
                    return Direction.RIGHT;
                }
                else return Direction.LEFT;
            }
            else
            {
                if ((target.Ypos - caller.Ypos) > 0)
                {
                    return Direction.DOWN;
                }
                else return Direction.UP;
            }

        }

        public  int getDistance(double x1, double x2, double y1, double y2)
        {
            int xDif = (int)Math.Abs(x1 - x2);
            int yDif = (int)Math.Abs(y1 - y2);
            return (int)Math.Sqrt(xDif * xDif + yDif * yDif);
        }

        public void Dispose()
        {
            Console.WriteLine("Game Engine disposed");
        }
    }
}
