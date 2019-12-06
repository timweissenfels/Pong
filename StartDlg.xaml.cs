using System;
using System.Windows;

namespace Pong
{
    /// <summary>
    /// Interaktionslogik für StartDlg.xaml
    /// </summary>
    public partial class StartDlg : Window
    {
        public Double Radius { get; set; }

        public StartDlg()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Radius = Convert.ToDouble(radius.Text);

                if (Radius < 1 || Radius > 100)
                {
                    throw new Exception("Der Wert muss zwischen 1 und 100 einschließlich liegen.");
                }
                else
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message, "Eingabefehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
