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

        public Ball(Double X = 100, Double Y = 100, Double Vx = 50, Double Vy = 80, Double Radius = 10)
        {
            this.X = X;
            this.Y = Y;
            this.Vx = Vx;
            this.Vy = Vy;
            this.Radius = Radius;

            Elli = new Ellipse
            {
                Width = 2 * Radius,
                Height = 2 * Radius,
                Fill = Brushes.Blue
            };

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

        public void Move(Double dt)
        {
            X = X + Vx * dt / 1000;
            Y = Y + Vy * dt / 1000;
           // return new Pair<double, double>(X, Y);
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
            if (X >= Canvas.GetLeft(p.Player_rec) - 1 * Radius && X <= Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width + 1 * Radius)
            {
                if (Y + Radius >= Canvas.GetTop(p.Player_rec) && Y - Radius <= Canvas.GetTop(p.Player_rec))
                {
                    Vy = -Vy;
                    Y -= 2.0 * (Y + Radius - Canvas.GetTop(p.Player_rec));
                }
                else if (Y - Radius <= Canvas.GetTop(p.Player_rec) + p.Player_rec.Height && Y + Radius >= Canvas.GetTop(p.Player_rec) + p.Player_rec.Height)
                {
                    Vy = -Vy;
                    Y += 2.0 * (Canvas.GetTop(p.Player_rec) + p.Player_rec.Height - (Y - Radius));
                }
            }
            if (Y >= Canvas.GetTop(p.Player_rec) - 1 * Radius && Y <= Canvas.GetTop(p.Player_rec) + p.Player_rec.Height + 1 * Radius)
            {
                if (X + Radius >= Canvas.GetLeft(p.Player_rec) && X - Radius <= Canvas.GetLeft(p.Player_rec))
                {
                    Vx = -Vx;
                    X -= 2.0 * (X + Radius - Canvas.GetLeft(p.Player_rec));
                }
                else if (X - Radius <= Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width && X + Radius >= Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width)
                {
                    Vx = -Vx;
                    X += 2.0 * (Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width - (X - Radius));
                }
            }
            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }
    }
}