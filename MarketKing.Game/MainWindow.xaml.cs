using DemoStrategy;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketKing.Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var engine = new Engine(GetStrategies(100), new Render(canvas, zoom, Dispatcher));
            engine.RunStartSequence();
        }

        private IStrategy[] GetStrategies(int count)
        {
            var strategies = new RandomStrategy[count];
            for (int i = 0; i < count; i++)
                strategies[i] = new RandomStrategy();

            return strategies;
        }
    }
}
