using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
   public  class Plate : GameObject
    {
        bool passable;

        public const float length = 0.5f;
        public Plate[] diretions = new Plate[4];
      
        public List<GameObject> ObjOnPlate = new List<GameObject>();

        public bool Passable { get => passable; set => passable = value; }


        public Plate()
        { }


        public Plate(double _Xpos, double _Ypos, bool passable)
        {
            Xpos = _Xpos;
            Ypos = _Ypos;
            this.passable = passable;
        }

        public override void Spawn(int x, int y)
        {
        }

        public override void Disable()
        {
        }
    }
}
