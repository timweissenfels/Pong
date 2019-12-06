using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static Pong.Paddle;

namespace Pong
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartDlg dlg;
        Ball ball;
        Paddle p1;
        Paddle p2;

        DispatcherTimer timer;
        Double ticks_old;

        public MainWindow()
        {
            dlg = new StartDlg();

            if ((bool)dlg.ShowDialog())
            {
                InitializeComponent();
            }
            else
            {
                Close();
            }
            this.KeyDown += MainWindow_KeyDown;
            this.KeyUp += MainWindow_KeyUp;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W)) p1.DIR = Paddle.MOVE_DIR.UP;
            else if (Keyboard.IsKeyDown(Key.S)) p1.DIR = Paddle.MOVE_DIR.DOWN;

            if (Keyboard.IsKeyDown(Key.W) && Keyboard.IsKeyDown(Key.S))
                p1.DIR = Paddle.MOVE_DIR.STOP;

            if (Keyboard.IsKeyDown(Key.Up)) p2.DIR = Paddle.MOVE_DIR.UP;
            else if (Keyboard.IsKeyDown(Key.Down)) p2.DIR = Paddle.MOVE_DIR.DOWN;

            if (Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Down))
                p2.DIR = Paddle.MOVE_DIR.STOP;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyUp(Key.W) && Keyboard.IsKeyUp(Key.S))
               p1.DIR = Paddle.MOVE_DIR.STOP;   

            if (Keyboard.IsKeyUp(Key.Up) && Keyboard.IsKeyUp(Key.Down))
                p2.DIR = Paddle.MOVE_DIR.STOP;
        }

        private void wnd_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);

            ball = new Ball(250, 150, 200, 200, dlg.Radius);
            p1 = new Paddle(__X:Canvas.GetLeft(Rect) + 80, __pType: p_type.PLAYER);
            p2 = new Paddle(__X:Canvas.GetLeft(Rect) + Rect.Width - 100);

            p1.Draw(Cvs);
            p2.Draw(Cvs);
            ball.Draw(Cvs);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;
            ball.Move(ticks - ticks_old);
            p1.Move(ball);
            p2.Move(ball);
            ball.Collision(Rect);
            ball.Collision_paddle(p1);
            ball.Collision_paddle(p2);

            p1.Collison(Rect);
            p2.Collison(Rect);

            ticks_old = ticks;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            ticks_old = Environment.TickCount;

            timer.Start();
        }

        private void ende_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void parameter_Click(object sender, RoutedEventArgs e)
        {
            dlg = new StartDlg();

            if ((bool)dlg.ShowDialog())
            {
                ball.UnDraw(Cvs);
                ball = new Ball(250, 150, 200, 200, dlg.Radius);
                ball.Draw(Cvs);
            }
        }

        private void Cvs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                ball.Resize(sx, sy);
                p1.Resize(sy,sy);
                p2.Resize(sx,sy);

                Rect.Width *= sx;
                Rect.Height *= sy;
                Canvas.SetLeft(Rect, sx * Canvas.GetLeft(Rect));
                Canvas.SetTop(Rect, sy * Canvas.GetTop(Rect));
            }
            catch { }
        }
    }
}
