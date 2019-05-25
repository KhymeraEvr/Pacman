using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pacman
{
    public class Maze
    {
        public Plate[,] maze;
        public int CoinsCount = 0;
        public bool[,] coins;
        public bool gameStarted = false;
        public Mechanics.GameTimer timer = new Mechanics.GameTimer();
        public int Length { get; set; }
        public int Width { get; set; }
        public int FrameRate { get; set; }
        public Cherry[] Chers { get; set; } = new Cherry[9];
        public bool GameOver { get; set; } = false;
        public GameEngine Game { get; set; }

        public void fillMaze(int[,] matrixMaze)
        {
            Width = matrixMaze.GetUpperBound(0) - matrixMaze.GetLowerBound(0) + 1;
            Length = matrixMaze.GetUpperBound(1) - matrixMaze.GetLowerBound(1) + 1;
            maze = new Plate[Width, Length];
            coins = new bool[Width, Length];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    maze[i, j] = new Plate();
                }
            }
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    maze[i, j].Xpos = j;
                    maze[i, j].Ypos = i;
                    if (matrixMaze[i, j] == 0)
                    {
                        maze[i, j].Passable = true;
                        coins[i, j] = true;
                        if (maze[i, j].ObjOnPlate.Count == 0) maze[i, j].ObjOnPlate.Add(new Coin(i, j, Game));
                        CoinsCount++;
                    }
                    else
                    {
                        maze[i, j].Passable = false;
                    }
                    if (i != 0)
                    {//downPlate = 2
                        maze[i - 1, j].diretions[2] = maze[i, j];
                    }
                    if (i != Width - 1)
                    {//upperPlate = 0
                        maze[i + 1, j].diretions[0] = maze[i, j];
                    }
                    if (j != Length - 1)
                    {//leftPlate = 1
                        maze[i, j + 1].diretions[1] = maze[i, j];
                    }
                    if (j != 0)
                    {//rightPlate = 3
                        maze[i, j - 1].diretions[3] = maze[i, j];
                    }
                }
                int tunl = Game.Map.TunnelY;
                maze[tunl, 0].diretions[1] = maze[tunl, 27];
                maze[tunl, 0].diretions[0] = maze[tunl - 1, 27];
                maze[tunl, 0].diretions[2] = maze[tunl + 1, 27];
                maze[tunl, 0].diretions[3] = maze[tunl, 1];
                maze[tunl, 27].diretions[3] = maze[tunl, 0];
                maze[tunl, 27].diretions[1] = maze[tunl, 26];
                maze[tunl, 27].diretions[2] = maze[tunl + 1, 27];
                maze[tunl, 27].diretions[0] = maze[tunl - 1, 27];

            }

        }

        public void addCheries()
        {

            for (int i = 0; i < 7; i++)
            {
                Chers[i] = new Cherry(Game);
            }
            Chers[0].Spawn(1, Length - 2);
            Chers[1].Spawn(Width - 2, 1);
            Chers[2].Spawn(1, 1);
            Chers[3].Spawn(Width - 2, Length - 2);
            Chers[4].Spawn(11, Length / 2);
            Chers[5].Spawn(20, 12);
            Chers[6].Spawn(5, 12);
        }


        public void runMaze()
        {
            if (!GameOver)
            {
                Game.Score = 0;
                Player pl = Game.Player;
                pl.LivesRemain = 3;
                pl.Spawn((int)pl.respawnPos.Xpos, (int)pl.respawnPos.Ypos);
                Game.GhostsCol.spawnAllGhosts();
                while (!GameOver)
                {
                    if (gameStarted)
                    {
                        if (!timer.timer.Enabled)
                        {
                            timer.Start();
                        }

                        Game.eventsCheck();

                        if (pl.IsActive) pl.startMoving();
                        foreach (MovableObject ghost in Game.GhostsCol.Ghosts)
                        {
                            if (ghost.IsActive) ghost.startMoving();
                        }
                    }
                    Thread.Sleep(20);
                    Game.coresModel.Update();
                }
            }
            Console.WriteLine("MazeStopped");
            return;
        }
       

        public void consMaze(Player pl, GhostCollcection ghosts)
        {
            Console.CursorVisible = false;
            pl.Spawn(13, 17);
            Game.GhostsCol.spawnAllGhosts();
            addCheries();

            char ch;

            while (true)
            {
                if (gameStarted)
                {
                    if (!timer.timer.Enabled)
                    {

                        timer.Start();
                    }
                    Game.eventsCheck();
                    if (pl.IsActive) pl.startMoving();

                    foreach (MovableObject ghost in ghosts)
                    {
                        if (ghost.IsActive) ghost.startMoving();
                    }
                }

                System.Threading.Thread.Sleep(FrameRate);

                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Console.WriteLine("ChaseMode:  " + timer.ChaseMode);
                Console.WriteLine("Lives:  " + pl.LivesRemain);
                // Console.WriteLine("Player " + (float)pl.Xpos + "  " + (float)pl.Ypos);
                Console.WriteLine("Shadow " + (float)ghosts[0].Xpos + "  " + (float)ghosts[0].Ypos);
                Console.WriteLine("Speedy  " + (float)ghosts[1].Xpos + "  " + (float)ghosts[1].Ypos);
                Console.WriteLine("Bashfull " + (float)ghosts[2].Xpos + "  " + (float)ghosts[2].Ypos);
                Console.WriteLine("Pokey  " + (float)ghosts[3].Xpos + "  " + (float)ghosts[3].Ypos);
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (maze[i, j].ObjOnPlate.Count != 0)
                        {
                            Console.ForegroundColor = GetTypeColor(maze[i, j].ObjOnPlate[maze[i, j].ObjOnPlate.Count - 1].GetType().ToString());
                        }
                        ch = getSymb(maze[i, j]);
                        Console.Write(ch);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Score:  " + Game.Score);
            }
        }

        char getSymb(Plate pl)
        {
            if (pl.ObjOnPlate.Count == 0)
            {
                if (pl.Passable) return ' ';
                else return '#';
            }
            else
            {
                switch (pl.ObjOnPlate[pl.ObjOnPlate.Count - 1].GetType().ToString())
                {
                    case "Pacman.Player":
                        return '0';
                    case "Pacman.Monsters.Bashfull":
                        return '0';
                    case "Pacman.Shadow":
                        return '0';
                    case "Pacman.Monsters.Speedy":
                        return '0';
                    case "Pacman.Monsters.Pokey":
                        return '0';
                    case "Pacman.Coin":
                        return '.';
                    case "Pacman.Cherry":
                        return 'o';
                    default:
                        return '?';
                }
            }
        }

        ConsoleColor GetTypeColor(string type)
        {
            switch (type)
            {
                case "Pacman.Player":
                    return ConsoleColor.Green;
                case "Pacman.Shadow":
                    return ConsoleColor.Red;
                case "Pacman.Monsters.Speedy":
                    return ConsoleColor.Magenta;
                case "Pacman.Monsters.Bashfull":
                    return ConsoleColor.Cyan;
                case "Pacman.Monsters.Pokey":
                    return ConsoleColor.DarkYellow;
                case "Pacman.Coin":
                    return ConsoleColor.Yellow;
                case "Pacman.Cherry":
                    return ConsoleColor.DarkRed;
                default: return ConsoleColor.White;
            }
        }
    }
}

