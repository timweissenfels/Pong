using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pong
{
    class Paddle
    {
        public enum MOVE_DIR { UP = 0, DOWN = 1, STOP = 2 }
        public Rectangle Rec { get; set; }

        private double x;
        private double y;
        private double vY;

        public enum p_type
        {
            COMPUTER = 1,
            PLAYER = 2
        }

        public MOVE_DIR DIR = MOVE_DIR.STOP;
        public p_type PType = p_type.COMPUTER;
        public double X
        {
            set
            {
                this.x = value; Canvas.SetLeft(Rec, x);
            }
            get { return x; }
        }
        public double Y
        {
            set
            {
                this.y = value; Canvas.SetTop(Rec, y);
            }
            get { return y; }
        }

        public double Height
        {
            get { return Rec.Height; }
            set { Rec.Height = value; }
        }
        public double Width
        {
            get { return Rec.Width; }
            set { Rec.Width = value; }
        }

        public Paddle(double __Height = 200, double __Width = 100, double __X = 150, double __Y = 60, double __vY = 10, p_type __pType = p_type.COMPUTER)
        {
            Rec = new Rectangle();

            this.Height = __Height;
            this.Width = __Width;
            this.X = __X;
            this.Y = __Y;
            this.vY = __vY;
            this.PType = __pType;
            Rec.Fill = Brushes.IndianRed;

            Canvas.SetTop(Rec, this.Y);
            Canvas.SetLeft(Rec, this.X);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Rec))
            {
                c.Children.Add(Rec);
            }
        }

        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(Rec))
            {
                c.Children.Remove(Rec);
            }
        }

        public void Resize(double sx, double sy)
        {
            this.X *= sx;
            this.Y *= sy;

            this.Width *= (sx + sy) / 2;
            this.Height *= (sx + sy) / 2;

            Canvas.SetLeft(Rec, sx * Canvas.GetLeft(Rec));
            Canvas.SetTop(Rec, sy * Canvas.GetTop(Rec));
        }

        public void Move(Ball playball)
        {
            if (PType == p_type.PLAYER)
            {
                if (DIR == MOVE_DIR.UP)
                    this.Y -= vY;
                else if (DIR == MOVE_DIR.DOWN)
                    this.Y += vY;
            }
            else if (PType == p_type.COMPUTER)
            {
                if(this.Y + this.Height/2 < playball.Y)
                    this.Y += vY;
                else if (this.Y + this.Height / 2 > playball.Y)
                    this.Y -= vY;
            }

        }

        public void Collison(Rectangle r)
        {
            if (this.Y <= Canvas.GetTop(r))
                this.Y = Canvas.GetTop(r);

            if (this.Y + this.Height >= Canvas.GetTop(r) + r.Height)
                this.Y = Canvas.GetTop(r) + r.Height - this.Height;
        }
    }
}