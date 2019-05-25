using System;
using System.Collections;
using System.Collections.Generic;

namespace Pacman
{
    public class GhostCollcection : IEnumerable
    {
        private const int ghostsNumber = 4;
        public string ConnectionId { get; set; }
        public GameEngine Game { get; set; }
        public List<Ghost> Ghosts { get; set; } = new List<Ghost>();

        public GhostCollcection(string id, GameEngine game)
        {
            Game = game;
            ConnectionId = id;
            CherryEaten.Actions.Add(new Tuple<string,Action>(ConnectionId,scareAllGhosts));
            Mechanics.PlayerEaten.Actions.Add(new Tuple<string,Action>(ConnectionId,respawnGhosts));
            Game.Maze1.timer.ScareOverActions.Add(calmDownGhosts);
        }

        public void spawnAllGhosts()
        {
            foreach(Ghost gh in Ghosts)
            {
                gh.Spawn((int)gh.RespawnPos.Xpos, (int)gh.RespawnPos.Ypos);
            }
        }

        public void Add(Ghost ghost)
        {
            Ghosts.Add(ghost);
        }

        public void Remove(Ghost ghost)
        {
            Ghosts.Remove(ghost);
        }

        public Ghost this[int index]
        {
            get
            {
                return Ghosts[index];
            }
        }

        private void scareAllGhosts()
        {
            foreach (Ghost gh in Ghosts)
            {
                if (gh.ActivPath) continue;
                gh.IsScared = true;
                gh.MoveSpeed = 0.025f;
                gh.MoveStrategy = gh.scaredStrategy;
               

            }
        }

        public void calmDownGhosts()
        {
            foreach (Ghost gh in Ghosts)
            {
                if (gh.ActivPath) continue;
                gh.IsScared = false;
                gh.MoveSpeed = 0.05f;
            }
        }

        public void setTarget(Player pl)
        {
            foreach(Ghost gh in Ghosts)
            {
                gh.Target = pl;
            }
        }

        public void respawnGhosts()
        {
            foreach(Ghost ghost in Ghosts)
            {
                ghost.Disable();
                ghost.Spawn((int)ghost.RespawnPos.Xpos, (int)ghost.RespawnPos.Ypos);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Ghosts.GetEnumerator();
        }
    }
}
