using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MarketKing.Game
{
    public class Hexagon
    {
        private Color _color;
        private Polygon _polygon;
        private Grid _grid;
        private TextBlock _text;
        private Dispatcher _uiDispatcher;
        private Cell _cell;
        private Dictionary<Cell, Hexagon> _lookup;
        public Hexagon(Color color, Dispatcher uiDispatcher, Cell cell, Dictionary<Cell, Hexagon> lookup)
        {
            _uiDispatcher = uiDispatcher;
            _lookup = lookup;
            _cell = cell;
            _polygon = new Polygon();
            _grid = new Grid();
            _color = color;
            _polygon.Points = new PointCollection(new List<Point>() {
                new Point(0,GameConfig.StepSize*GameConfig.HeightRatio/2),
                new Point(GameConfig.StepSize-5,0),
                new Point(GameConfig.StepSize*2+5,0),
                new Point(GameConfig.StepSize*3,GameConfig.StepSize*GameConfig.HeightRatio/2),
                new Point(GameConfig.StepSize*2+5,GameConfig.StepSize*GameConfig.HeightRatio),
                new Point(GameConfig.StepSize-5,GameConfig.StepSize*GameConfig.HeightRatio),
                new Point(0,GameConfig.StepSize*GameConfig.HeightRatio/2)
        });
            _polygon.Fill = new SolidColorBrush(_color);
            _polygon.Loaded += Hexagon_Loaded;
            // used for debug mode only
            //_polygon.MouseEnter += Hexagon_MouseEnter;
            //_polygon.MouseLeave += Hexagon_MouseLeave;
            _polygon.Stroke = new SolidColorBrush(Colors.Black);
            _polygon.StrokeThickness = 1;

            _grid.Children.Add(_polygon);

            _text = new TextBlock();
            _text.Text = "0";
            _text.FontSize = 16;
            _text.HorizontalAlignment = HorizontalAlignment.Center;
            _text.VerticalAlignment = VerticalAlignment.Center;
            _grid.Children.Add(_text);
        }

        public FrameworkElement Control
        {
            get { return _grid; }
        }

        public void SetColor(Color color) => _uiDispatcher.InvokeAsync(() => AnimateColor(color, 200));
        public void SetValue(int value) => _uiDispatcher.InvokeAsync(() => _text.Text = value.ToString());

        private void Hexagon_MouseLeave(object sender, MouseEventArgs e)
        {
            SetColor(Colors.White);
            if (_cell.TopLeft != null)
                _lookup[_cell.TopLeft]?.SetColor(Colors.White);
            if (_cell.Top != null)
                _lookup[_cell.Top]?.SetColor(Colors.White);
            if (_cell.TopRight != null)
                _lookup[_cell.TopRight]?.SetColor(Colors.White);
            if (_cell.BottomRight != null)
                _lookup[_cell.BottomRight]?.SetColor(Colors.White);
            if (_cell.Bottom != null)
                _lookup[_cell.Bottom]?.SetColor(Colors.White);
            if (_cell.BottomLeft != null)
                _lookup[_cell.BottomLeft]?.SetColor(Colors.White);
        }
        private void Hexagon_MouseEnter(object sender, MouseEventArgs e)
        {
            SetColor(Colors.Red);
            if (_cell.TopLeft != null)
                _lookup[_cell.TopLeft]?.SetColor(Colors.Blue);
            if (_cell.Top != null)
                _lookup[_cell.Top]?.SetColor(Colors.Blue);
            if (_cell.TopRight != null)
                _lookup[_cell.TopRight]?.SetColor(Colors.Blue);
            if (_cell.BottomRight != null)
                _lookup[_cell.BottomRight]?.SetColor(Colors.Blue);
            if (_cell.Bottom != null)
                _lookup[_cell.Bottom]?.SetColor(Colors.Blue);
            if (_cell.BottomLeft != null)
                _lookup[_cell.BottomLeft]?.SetColor(Colors.Blue);
        }

        private void Hexagon_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void AnimateColor(Color toColor, int miliseconds)
        {
            ColorAnimation colorChangeAnimation = new ColorAnimation();
            colorChangeAnimation.From = _color;
            colorChangeAnimation.To = toColor;
            colorChangeAnimation.Duration = TimeSpan.FromMilliseconds(miliseconds);

            PropertyPath colorTargetPath = new PropertyPath("(Polygon.Fill).(SolidColorBrush.Color)");
            Storyboard CellBackgroundChangeStory = new Storyboard();
            Storyboard.SetTarget(colorChangeAnimation, _polygon);
            Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
            CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
            CellBackgroundChangeStory.Begin();
            _color = toColor;
        }
    }
}
