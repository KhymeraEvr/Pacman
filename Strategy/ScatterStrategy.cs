using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Strategy
{
    public class ScatterStrategy : IMoveStragegy
    {
        public bool needRefresh = false;
        Queue<Plate> path;

        public Ghost _ghost;
        readonly int _zoneX;
        readonly int _zoneY;
        bool isInCorner = false;

        public ScatterStrategy(Ghost ghost, int zoneX, int zoneY)
        {
            _ghost = ghost;
            _zoneY = zoneY;
            _zoneX = zoneX;
        }


        public void Refresh()
        {
            Mechanics.BreadthSearch search = new Mechanics.BreadthSearch(_ghost.Game);
            path = search.getPath(_ghost.CurrentPlate, _ghost.Game.Maze1.maze[_zoneY, _zoneX]);
            isInCorner = false;
            LookAhead();
        }

        public void LookAhead()
        {
            
            if(needRefresh)
            {
                needRefresh = false;
                Refresh();
                return;
            }
            if (path == null)
            {
                Mechanics.BreadthSearch search = new Mechanics.BreadthSearch(_ghost.Game);
                path = search.getPath(_ghost.CurrentPlate, _ghost.Game.Maze1.maze[_zoneY, _zoneX]);
            }
            if (!isInCorner && path.Count <= 1)
            {
                isInCorner = true;
            }
            if (!isInCorner)
            {
              //if (path.Count == 1) isInCorner = true;
                _ghost.nextPlate = path.Dequeue();
                for (int i = 0; i < _ghost.CurrentPlate.diretions.Length; i++)
                {
                    if (_ghost.CurrentPlate.diretions[i] == _ghost.nextPlate)
                    {
                        _ghost.Direction = (Direction)i;
                    }
                }
            }
            else
            {
                if (_ghost.CurrentPlate.diretions[_ghost.NextTurn].Passable)
                {
                    _ghost.nextPlate = _ghost.CurrentPlate.diretions[_ghost.NextTurn];
                    _ghost.Direction = (Direction)_ghost.NextTurn;
                    _ghost.NextTurn = (_ghost.NextTurn + 1) % 4;
                }
                else if (_ghost.CurrentPlate.diretions[(_ghost.NextTurn + 2) % 4].Passable)
                {
                    _ghost.NextTurn = (_ghost.NextTurn + 2) % 4;
                    _ghost.nextPlate = _ghost.CurrentPlate.diretions[_ghost.NextTurn];
                    _ghost.Direction = (Direction)_ghost.NextTurn;
                    _ghost.NextTurn = (_ghost.NextTurn + 1) % 4;

                }
                else _ghost.nextPlate = _ghost.CurrentPlate.diretions[(int)_ghost.Direction];
            }
        }
    }
}

