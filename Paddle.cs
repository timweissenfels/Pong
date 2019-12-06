using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pong
{
    class Paddle
    {
        public enum MOVE_DIR { STOP = 0, UP = 1, DOWN = 2, Right = 3, Left = 4 }
        public enum p_type { COMPUTER = 1, PLAYER = 2 }

        public enum p_level { NONE = 100, EASY = 10, MEDIUM = 1, UNBEATABLE = 1}
        public Rectangle Player_rec { get; set; }

        private double x;
        private double y;
        private double vY;
        private double vX;

        public MOVE_DIR DIR = MOVE_DIR.STOP;
        public p_type PType = p_type.COMPUTER;
        private p_level p_Level = p_level.MEDIUM;

        public double X
        {
            set
            {
                this.x = value; Canvas.SetLeft(Player_rec, x);
            }
            get { return x; }
        }
        public double Y
        {
            set
            {
                this.y = value; Canvas.SetTop(Player_rec, y);
            }
            get { return y; }
        }

        public double Height
        {
            get { return Player_rec.Height; }
            set { Player_rec.Height = value; }
        }
        public double Width
        {
            get { return Player_rec.Width; }
            set { Player_rec.Width = value; }
        }

        public Paddle(double __Height = 200, double __Width = 20, double __X = 150, double __Y = 60, double __vY = 10, double __vX = 10, p_type __pType = p_type.COMPUTER,p_level __p_Level = p_level.MEDIUM)
        {
            Player_rec = new Rectangle();

            this.Height = __Height;
            this.Width = __Width;
            this.X = __X;
            this.Y = __Y;
            this.vY = __vY;
            this.vX = __vX;
            this.PType = __pType;
            this.p_Level = __p_Level;

            Player_rec.Fill = Brushes.Black;

            Canvas.SetTop(Player_rec, this.Y);
            Canvas.SetLeft(Player_rec, this.X);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Player_rec))
            {
                c.Children.Add(Player_rec);
            }
        }

        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(Player_rec))
            {
                c.Children.Remove(Player_rec);
            }
        }

        public void Move(Ball playball)
        {
            if (PType == p_type.PLAYER)
            {
                if (DIR == MOVE_DIR.UP)
                    this.Y -= this.vY;
                else if (DIR == MOVE_DIR.DOWN)
                    this.Y += this.vY;

                if (DIR == MOVE_DIR.Left)
                    this.X -= this.vX;
                else if (DIR == MOVE_DIR.Right)
                    this.X += this.vX;

            }
            else if (PType == p_type.COMPUTER)
            {
                if(this.Y + this.Height/2 < playball.Y)
                    this.Y += vY/(double)p_Level;
                else if (this.Y + this.Height / 2 > playball.Y)
                    this.Y -= vY / (double)p_Level;
            }
        }

        public void Collison(Rectangle r, Rectangle midrec)
        {
            if (this.Y <= Canvas.GetTop(r))
                this.Y = Canvas.GetTop(r);

            if (this.Y + this.Height >= Canvas.GetTop(r) + r.Height)
                this.Y = Canvas.GetTop(r) + r.Height - this.Height;

            if (this.X <= Canvas.GetLeft(r))
                this.X = Canvas.GetLeft(r);

            if (this.X + this.Width >= Canvas.GetLeft(r) + r.Width)
                this.X = Canvas.GetTop(r) + r.Width - this.Width;

            if(Canvas.GetLeft(this.Player_rec) + this.Width >= Canvas.GetLeft(midrec) && Canvas.GetLeft(this.Player_rec) < Canvas.GetLeft(midrec))
                this.X = Canvas.GetLeft(midrec) + midrec.Width - this.Width;


        }
    }
}