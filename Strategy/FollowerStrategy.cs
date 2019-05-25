using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman.Monsters;

namespace Pacman.Strategy
{
    public class FollowerStrategy : IMoveStragegy
    {
        Ghost _ghost;
        int lookAheadNumb;
        delegate Plate PlateSearch(int a);
        PlateSearch target;

        public FollowerStrategy() { }
        public FollowerStrategy(Func<int, Plate> a, Ghost ghost, int lookAhead)
        {
            _ghost = ghost;
            lookAheadNumb = lookAhead;
            target = new PlateSearch(a);
        }

        public void setTarget(Func<int, Plate> a)
        {
            target = new PlateSearch(a);
        }

        public void LookAhead()
        {
            if (!_ghost.ActivPath) _ghost.getToPosition(target.Invoke(lookAheadNumb));
            FollowPath();
        }
        public void FollowPath()
        {
            if (_ghost.Path.Count == 0)
            {
                if (_ghost.ActivPath)
                {
                    if(_ghost.Game.getDistance(_ghost.Xpos,_ghost.RespawnPos.Xpos,_ghost.Ypos,_ghost.RespawnPos.Ypos) <= 5)
                    {
                        _ghost.returnToGame();
                        return;
                    }
                    if (_ghost.CurrentPlate == _ghost.RespawnPos)
                    {
                        _ghost.returnToGame();
                        return;
                    }
                    else
                    {
                        Mechanics.BreadthSearch aStar = new Mechanics.BreadthSearch(_ghost.Game);
                        _ghost.Path = aStar.getPath(_ghost.CurrentPlate, _ghost.RespawnPos);                                 
                        _ghost.FollowPath();
                        return;
                    }
                }
                _ghost.findPlate((int)_ghost.Direction);
                return;
            }
            _ghost.nextPlate = _ghost.Path.Dequeue();
            for (int i = 0; i < _ghost.CurrentPlate.diretions.Length; i++)
            {
                if (_ghost.CurrentPlate.diretions[i] == _ghost.nextPlate)
                {
                    _ghost.Direction = (Direction)i;
                }
            }
        }

        public void getToPosition(Plate destination)
        {
            Mechanics.BreadthSearch aStar = new Mechanics.BreadthSearch(_ghost.Game);
            _ghost.Path = aStar.getPath(_ghost.CurrentPlate, destination);
        }

        public void Refresh()
        {
            Mechanics.BreadthSearch aStar = new Mechanics.BreadthSearch(_ghost.Game);
            _ghost.Path = aStar.getPath(_ghost.CurrentPlate, target.Invoke(0));
        }
    }
}