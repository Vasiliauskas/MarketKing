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
        private readonly Engine _engine;
        private readonly Render _render;
        public MainWindow()
        {
            InitializeComponent();
            _render = new Render(canvas, zoom, Dispatcher);
            _engine = new Engine(GetStrategies(10), _render);
            _engine.RunStartSequence();
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
