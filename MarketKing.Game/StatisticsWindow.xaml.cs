using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MarketKing.Game
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown;

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F4:
                    Application.Current.Shutdown();
                    break;
                case Key.F11:
                    ToggleFullscreen();
                    break;
            }

            e.Handled = true;
        }

        private void ToggleFullscreen()
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResize;
            }
        }
    }
}
