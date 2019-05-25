using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Pacman
{


    public class Program
    {
        public static Dictionary<string, GameEngine> games = new Dictionary<string, GameEngine>();

        public static void Setup(string gameId, int mapId)
        {
            GameEngine game;
            if (games.Keys.Contains(gameId))
            {
                games[gameId].MapInit(mapId);
                games[gameId].mapJSON = games[gameId].Map.MapJSON;
                games[gameId].Maze1.fillMaze(games[gameId].Map.maze);
                games[gameId].Maze1.addCheries();
                games[gameId].Maze1.GameOver = false;
                games[gameId].RunMaze();
                return;
            }
            game = new GameEngine();
            game.GameId = gameId;
            game.MapInit(mapId);
            game.mapJSON = game.Map.MapJSON;
            int[,] testMazeMat = game.Map.maze;

            game.Maze1 = new Maze();
            game.Maze1.Game = game;
            game.Maze1.timer.Game = game;
            game.Maze1.fillMaze(testMazeMat);
            game.Maze1.addCheries();

            Player pl = new Player(game);
            game.Player = pl;
            Shadow shadow = new Shadow(game);
            Monsters.Speedy speedy = new Monsters.Speedy(game);
            Monsters.Bashfull bashfull = new Monsters.Bashfull(game);
            Monsters.Pokey pokey = new Monsters.Pokey(game);
            GhostCollcection ghosts = new GhostCollcection(gameId, game);
            ghosts.Add(shadow);
            ghosts.Add(speedy);
            ghosts.Add(bashfull);
            ghosts.Add(pokey);
            ghosts.setTarget(pl);
            game.GhostsCol = ghosts;
            game.startGame();
            games.Add(gameId, game);

        }
        public static void Main(string[] args)
        {
        }
    }
}
