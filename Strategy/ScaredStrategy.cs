using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Strategy
{
    public class ScaredStrategy : IMoveStragegy
    {
        Ghost _ghost;

        public ScaredStrategy(Ghost ghost)
        {
            _ghost = ghost;
        }
        public void LookAhead()
        {
            int direction = (int)_ghost.Direction;
            Direction playerDir = _ghost.Target.Game.FindTargetDirection(_ghost.Target, _ghost);
            playerDir = (Direction)((int)(playerDir + 2) % 4);

            if (_ghost.CurrentPlate.diretions[(int)playerDir].Passable)
            {
                _ghost.nextPlate = _ghost.CurrentPlate.diretions[(int)playerDir];
                _ghost.Direction = playerDir;
            }
            else if (_ghost.CurrentPlate.diretions[direction].Passable)
            {
                _ghost.nextPlate = _ghost.CurrentPlate.diretions[direction];
                return;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != direction && _ghost.CurrentPlate.diretions[i].Passable)
                    {
                        if (i == (int)_ghost.Target.Game.FindTargetDirection(_ghost.Target, _ghost)) continue;
                        _ghost.nextPlate = _ghost.CurrentPlate.diretions[i];
                        _ghost.Direction = (Direction)i;
                    }
                }
            }
        }

        public void Refresh()
        {
        
        }
    }
}

