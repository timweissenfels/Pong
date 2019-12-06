using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pong
{
    class Ball
    {
        public Ellipse Elli { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Vx { get; set; }
        public Double Vy { get; set; }
        public Double Radius { get; set; }

        public Ball(Double X = 100, Double Y = 100, Double Vx = 20, Double Vy = 50, Double Radius = 10)
        {
            this.X = X;
            this.Y = Y;
            this.Vx = Vx;
            this.Vy = Vy;
            this.Radius = Radius;

            Elli = new Ellipse();
            Elli.Width = 2 * Radius;
            Elli.Height = 2 * Radius;
            Elli.Fill = Brushes.Blue;

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Elli))
            {
                c.Children.Add(Elli);
            }
        }

        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(Elli))
            {
                c.Children.Remove(Elli);
            }
        }

        public void Resize(double sx, double sy)
        {
            X *= sx;
            Y *= sy;

            Vx *= sx;
            Vy *= sy;

            Radius *= (sx + sy) / 2;

            Elli.Width *= (sx + sy) / 2;
            Elli.Height *= (sx + sy) / 2;

            Canvas.SetLeft(Elli, sx * Canvas.GetLeft(Elli));
            Canvas.SetTop(Elli, sy * Canvas.GetTop(Elli));
        }

        public Pair<Double, Double> Move(Double dt)
        {
            X = X + Vx * dt / 1000;
            Y = Y + Vy * dt / 1000;
            return new Pair<double, double>(X, Y);
        }

        public void Collision(Rectangle r)
        {
            // Obere oder untere Bande
            if (Y - Radius <= Canvas.GetTop(r))
            {
                Vy = -Vy;                                       // Reflexion and der Bande
                Y = Y + 2 * (Canvas.GetTop(r) - (Y - Radius));  // Korrektur des Detektionsfehlers
            }
            else if (Y + Radius >= Canvas.GetTop(r) + r.Height)
            {
                Vy = -Vy;
                Y = Y - 2 * (Y + Radius - Canvas.GetTop(r) - r.Height);
            }

            // Linke oder rechte Bande
            if (X - Radius <= Canvas.GetLeft(r))
            {
                Vx = -Vx;
                X = X + 2 * (Canvas.GetLeft(r) - (X - Radius));
            }
            else if (X + Radius >= Canvas.GetLeft(r) + r.Width)
            {
                Vx = -Vx;
                X = X - 2 * (X + Radius - Canvas.GetLeft(r) - r.Width);
            }

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }
        public void Collision_paddle(Paddle p)
        {
            Rectangle rect = p.Rec;
            if (this.X >= Canvas.GetLeft((UIElement)rect) - 1 * this.Radius && this.X <= Canvas.GetLeft((UIElement)rect) + rect.Width + 1 * this.Radius)
            {
                if (this.Y + this.Radius >= Canvas.GetTop((UIElement)rect) && this.Y - this.Radius <= Canvas.GetTop((UIElement)rect))
                {
                    this.Vy = -this.Vy;
                    this.Y -= 2.0 * (this.Y + this.Radius - Canvas.GetTop((UIElement)rect));
                }
                else if (this.Y - this.Radius <= Canvas.GetTop((UIElement)rect) + rect.Height && this.Y + this.Radius >= Canvas.GetTop((UIElement)rect) + rect.Height)
                {
                    this.Vy = -this.Vy;
                    this.Y += 2.0 * (Canvas.GetTop((UIElement)rect) + rect.Height - (this.Y - this.Radius));
                }
            }
            if (this.Y >= Canvas.GetTop((UIElement)rect) - 1 * this.Radius && this.Y <= Canvas.GetTop((UIElement)rect) + rect.Height + 1 * this.Radius)
            {
                if (this.X + this.Radius >= Canvas.GetLeft((UIElement)rect) && this.X - this.Radius <= Canvas.GetLeft((UIElement)rect))
                {
                    this.Vx = -this.Vx;
                    this.X -= 2.0 * (this.X + this.Radius - Canvas.GetLeft((UIElement)rect));
                }
                else if (this.X - this.Radius <= Canvas.GetLeft((UIElement)rect) + rect.Width && this.X + this.Radius >= Canvas.GetLeft((UIElement)rect) + rect.Width)
                {
                    this.Vx = -this.Vx;
                    this.X += 2.0 * (Canvas.GetLeft((UIElement)rect) + rect.Width - (this.X - this.Radius));
                }
            }
            Canvas.SetLeft((UIElement)this.Elli, this.X - this.Radius);
            Canvas.SetTop((UIElement)this.Elli, this.Y - this.Radius);
        }
    }
}




/*       struct paddle_fraction
        {
            public enum DIR_CHANGES { SIDE = 1, TOP = 2, EDGE = 3 };

            public DIR_CHANGES dir_change;
            public Rect fraction;
            public double speed_offset;
            public paddle_fraction(double X, double Y, double width, double height, double __speed_offset, DIR_CHANGES dc)
            {
                fraction = new Rect(X, Y, width, height);
                speed_offset = __speed_offset;
                dir_change = dc;
            }
        }
        private class pieces
        {
            public enum pices
            {
                edgetopleft = 0, edgetopright = 1, edgebottomleft = 2, edgebottomright = 3,
                
                sidetopleft = 4, sidemidleft = 5, sidebottomleft = 6,
                sidetopright = 7, sidemidright = 8, sidebottomright = 9,
                
                toptop = 10, topbottom = 11,
            };
        }

        public void Collision_paddle(Rectangle r)
        {
            paddle_fraction[] fractions = new paddle_fraction[12];
            var ball = new Rect(this.X,this.Y,Elli.Width,Elli.Height);
            double offset_quadrat = r.Width / 30;
            //ECKEN
            fractions[(int)pieces.pices.edgetopleft] = new paddle_fraction(Canvas.GetLeft(r) + offset_quadrat / 2, Canvas.GetTop(r) + offset_quadrat / 2, offset_quadrat, offset_quadrat, 0.5, paddle_fraction.DIR_CHANGES.EDGE);
            fractions[(int)pieces.pices.edgetopright] = new paddle_fraction(Canvas.GetLeft(r) + r.Width + offset_quadrat / 2, Canvas.GetTop(r) + offset_quadrat / 2, offset_quadrat, offset_quadrat, 0.5, paddle_fraction.DIR_CHANGES.EDGE);
            fractions[(int)pieces.pices.edgebottomleft] = new paddle_fraction(Canvas.GetLeft(r) + offset_quadrat / 2, Canvas.GetTop(r) + r.Height + offset_quadrat / 2, offset_quadrat, offset_quadrat, 0.5, paddle_fraction.DIR_CHANGES.EDGE);
            fractions[(int)pieces.pices.edgebottomright] = new paddle_fraction(Canvas.GetLeft(r) + r.Width + offset_quadrat / 2, Canvas.GetTop(r) + r.Height + offset_quadrat / 2, offset_quadrat, offset_quadrat, 0.5, paddle_fraction.DIR_CHANGES.EDGE);

            //Links Rechts
            
            fractions[(int)pieces.pices.sidetopright] = new paddle_fraction(Canvas.GetLeft(r) + r.Width - (r.Width / 3)/2, Canvas.GetTop(r) + offset_quadrat, r.Width / 3, r.Height - offset_quadrat*2, 1, paddle_fraction.DIR_CHANGES.SIDE);
            fractions[(int)pieces.pices.sidetopleft] = new paddle_fraction(Canvas.GetLeft(r) + (r.Width / 3)/2, Canvas.GetTop(r) + offset_quadrat, r.Width / 3, r.Height - offset_quadrat*2, 1, paddle_fraction.DIR_CHANGES.SIDE);

            //Oben Unten
            fractions[(int)pieces.pices.toptop] = new paddle_fraction(Canvas.GetLeft(r) + (r.Width / 2) + offset_quadrat, Canvas.GetTop(r) + r.Height, r.Width - offset_quadrat*2, r.Height / 10, 1, paddle_fraction.DIR_CHANGES.SIDE);
            fractions[(int)pieces.pices.topbottom] = new paddle_fraction(Canvas.GetLeft(r) + (r.Width / 2) + offset_quadrat, Canvas.GetTop(r) , r.Width - offset_quadrat*2, r.Height / 10, 1, paddle_fraction.DIR_CHANGES.SIDE);

            foreach (var piece in fractions)
            {
                if (ball.IntersectsWith(piece.fraction))
                {
                    if (piece.dir_change == paddle_fraction.DIR_CHANGES.SIDE)
                    {
                        Vx = -Vx;
                    }
                    else if (piece.dir_change == paddle_fraction.DIR_CHANGES.TOP)
                    {
                        Vy = -Vy;
                    }
                    else if (piece.dir_change == paddle_fraction.DIR_CHANGES.EDGE)
                    {
                        Vx = -Vx;
                        Vy = -Vy;
                    }
                }
            }
        }
*/