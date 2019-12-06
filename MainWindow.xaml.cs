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
            //if(Keyboard.IsKeyDown(Key.Escape))
            if(Keyboard.IsKeyDown(Key.Return))
                InitializeComponent();

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

            ball = new Ball(250, 150, 400, 400, dlg.Radius);
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

            check_move_DIR();

            p1.Move(ball);
            p2.Move(ball);
            ball.Collision(Rect, ref score_s1_value, ref score_s2_value);
            ball.Collision_paddle(p1);
            ball.Collision_paddle(p2);

            p1.Collison(Rect,midrect);
            p2.Collison(Rect,midrect);

            ticks_old = ticks;
        }

        private void check_move_DIR()
        {
            if (Keyboard.IsKeyDown(Key.W)) p1.DIR = Paddle.MOVE_DIR.UP;
            if (Keyboard.IsKeyDown(Key.S)) p1.DIR = Paddle.MOVE_DIR.DOWN;

            if (Keyboard.IsKeyDown(Key.Up)) p2.DIR = Paddle.MOVE_DIR.UP;
            if (Keyboard.IsKeyDown(Key.Down)) p2.DIR = Paddle.MOVE_DIR.DOWN;

            if (Keyboard.IsKeyDown(Key.A)) p1.DIR = Paddle.MOVE_DIR.Left;
            if (Keyboard.IsKeyDown(Key.D)) p1.DIR = Paddle.MOVE_DIR.Right;

            if (Keyboard.IsKeyDown(Key.Left)) p2.DIR = Paddle.MOVE_DIR.Left;
            if (Keyboard.IsKeyDown(Key.Right)) p2.DIR = Paddle.MOVE_DIR.Right;
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
                ball = new Ball(250, 150, 800, 800, dlg.Radius);
                ball.Draw(Cvs);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
