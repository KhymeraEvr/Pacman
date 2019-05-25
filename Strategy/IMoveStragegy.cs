using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Strategy
{
    public interface IMoveStragegy
    {
        void LookAhead();
        void Refresh();
    }
}
