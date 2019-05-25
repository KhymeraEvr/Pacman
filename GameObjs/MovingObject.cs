using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class MovableObject : GameObject
    {
        private Direction direction;
        private float moveSpeed = 0.05f;
        private Plate currentPlate;
        private Plate _nextPlate;
        private Direction nextDirection;

        public MovableObject()
        {

        }
        protected MovableObject(GameEngine game)
        {
            Game = game;
        }

        public Direction Direction { get => direction; set => direction = value; }
        public Plate CurrentPlate { get => currentPlate; set => currentPlate = value; }
        internal Plate nextPlate { get => _nextPlate; set => _nextPlate = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        internal Direction NextDirection { get => nextDirection; set => nextDirection = value; }
        public GameEngine Game { get; set; }


        virtual public void Move()
        {
            switch (Direction)
            {
                case Direction.UP:
                    Ypos -= MoveSpeed;
                    break;
                case Direction.RIGHT:
                    Xpos += MoveSpeed;
                    break;
                case Direction.DOWN:
                    Ypos += MoveSpeed;
                    break;
                case Direction.LEFT:
                    Xpos -= MoveSpeed;
                    break;
            }
            if (Xpos < -0.5)
            {
                currentPlate.ObjOnPlate.Remove(this);
                Spawn((int)(Game.Maze1.Length - 1), (int)Ypos);
                NextDirection = Direction.LEFT;
                lookAhead();
                
            }
            if (Xpos > (Game.Maze1.Length))
            {
                currentPlate.ObjOnPlate.Remove(this);

                Spawn(0, (int)Ypos);
                Direction = Direction.RIGHT;
                lookAhead();
            }
        }
        virtual public void lookAhead() { }
        virtual public void startMoving() { }
    }
}
