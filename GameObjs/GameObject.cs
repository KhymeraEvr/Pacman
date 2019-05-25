namespace Pacman
{
    public abstract class GameObject
    {
        private double xpos;
        private double ypos;
        private bool isActive;
        private float radius;

        protected GameObject() { }
        protected GameObject(double xpos, double ypos, bool isActive, float radius)
        {
            Xpos = xpos;
            Ypos = ypos;
            IsActive = isActive;
            Radius = radius;
        }

        public double Xpos { get => xpos; set => xpos = value; }
        public double Ypos { get => ypos; set => ypos = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public float Radius { get => radius; set => radius = value; }

        public abstract void Spawn(int x, int y);
        public abstract void Disable();
    }

   public enum Direction : int
    {
        UP = 0,
        LEFT,
        DOWN,
        RIGHT
    }

    enum KeyCode : int
    {

        Left = 0x25,

        Up,

        Right,

        Down
    }
}
