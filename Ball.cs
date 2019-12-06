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

        public bool Scored = false;

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
                Fill = Brushes.DarkBlue
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

        public void Move()
        {
            X += Vx;
            Y += Vy;
        }

        public void Collision(Rectangle r, ref Label lsp1, ref Label lsp2)
        {
            // Obere oder untere Bande
            if (Y - Radius <= Canvas.GetTop(r))
            {
                Vy = -Vy;                                       // Reflexion and der Bande
                Y += 2 * (Canvas.GetTop(r) - (Y - Radius));  // Korrektur des Detektionsfehlers
            }
            else if (Y + Radius >= Canvas.GetTop(r) + r.Height)
            {
                Vy = -Vy;
                Y -= 2 * (Y + Radius - Canvas.GetTop(r) - r.Height);
            }

            // Linke oder rechte Bande
            if (X - Radius <= Canvas.GetLeft(r))
            {
                Vx = -Vx;
                X += 2 * (Canvas.GetLeft(r) - (X - Radius));
                if (!this.Scored)
                {
                    int.TryParse(lsp1.Content.ToString(), out int value);
                    lsp1.Content = value + 1;
                }
                this.Elli.Fill = Brushes.Red;
                this.Scored = true;
            }
            else if (X + Radius >= Canvas.GetLeft(r) + r.Width)
            {
                Vx = -Vx;
                X -= 2 * (X + Radius - Canvas.GetLeft(r) - r.Width);
                if (!this.Scored)
                {
                    int.TryParse(lsp2.Content.ToString(), out int value);
                    lsp2.Content = value + 1;
                }
                this.Elli.Fill = Brushes.Red;
                this.Scored = true;
            }

            if(this.X - Radius > Canvas.GetLeft(r) + r.Width/5 && this.X + Radius < Canvas.GetLeft(r) + (r.Width-r.Width/5))
            {
                this.Elli.Fill = Brushes.DarkBlue;
                this.Scored = false;
            }


            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }
        public void Collision_paddle(Paddle p)
        {
            Collision_paddle_top(p);
            Collision_paddle_bottom(p);

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }
        public void Collision_paddle_top(Paddle p) 
        {
            double top_ball_point = this.Y - this.Radius;
            double bottom_ball_point = this.Y + this.Radius;

            if (X >= Canvas.GetLeft(p.Player_rec) - Radius && X <= Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width + Radius)
            {
                if (bottom_ball_point >= Canvas.GetTop(p.Player_rec) && top_ball_point <= Canvas.GetTop(p.Player_rec))
                {
                    Vy = -Vy;
                    Y -= 2 * (bottom_ball_point - Canvas.GetTop(p.Player_rec));
                }
                else if (Y - Radius <= Canvas.GetTop(p.Player_rec) + p.Player_rec.Height && bottom_ball_point >= Canvas.GetTop(p.Player_rec) + p.Player_rec.Height)
                {
                    Vy = -Vy;
                    Y += 2 * (Canvas.GetTop(p.Player_rec) + p.Player_rec.Height - top_ball_point);
                }
            }
        }
        private void Collision_paddle_bottom(Paddle p)
        {
            double left_ball_point = this.X - this.Radius;
            double right_ball_point = this.X + this.Radius;

            if (Y >= Canvas.GetTop(p.Player_rec) - Radius && Y <= Canvas.GetTop(p.Player_rec) + p.Player_rec.Height + Radius)
            {
                if (right_ball_point >= Canvas.GetLeft(p.Player_rec) && left_ball_point <= Canvas.GetLeft(p.Player_rec))
                {
                    Vx = -Vx;
                    X -= 2.0 * (right_ball_point - Canvas.GetLeft(p.Player_rec));
                }
                else if (left_ball_point <= Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width && right_ball_point >= Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width)
                {
                    Vx = -Vx;
                    X += 2.0 * (Canvas.GetLeft(p.Player_rec) + p.Player_rec.Width - left_ball_point);
                }
            }
        }
    }
}