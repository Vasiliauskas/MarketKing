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
        private readonly Color _color;
        private Polygon _polygon;
        private Grid _grid;
        private TextBlock _text;
        private Dispatcher _uiDispatcher;
        public Hexagon(Color color, Dispatcher uiDispatcher)
        {
            _uiDispatcher = uiDispatcher;
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
            _polygon.MouseEnter += Hexagon_MouseEnter;
            _polygon.MouseLeave += Hexagon_MouseLeave;
            _polygon.Stroke = new SolidColorBrush(Colors.Black);
            _polygon.StrokeThickness = 1;

            _grid.Children.Add(_polygon);

            //var dw = new DrawingVisual();
            //var context = dw.RenderOpen();
            //context.DrawText(new FormattedText())
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

        public void SetColor(Color color) => _uiDispatcher.InvokeAsync(() => _polygon.Fill = new SolidColorBrush(color));
        public void SetValue(int value) => _uiDispatcher.InvokeAsync(() => _text.Text = value.ToString());

        private void Hexagon_MouseLeave(object sender, MouseEventArgs e) => AnimateColor(Colors.Blue, _color, 300);
        private void Hexagon_MouseEnter(object sender, MouseEventArgs e) => AnimateColor(_color, Colors.Blue, 100);

        private void Hexagon_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void AnimateColor(Color fromColor, Color toColor, int miliseconds)
        {
            ColorAnimation colorChangeAnimation = new ColorAnimation();
            colorChangeAnimation.From = fromColor;
            colorChangeAnimation.To = toColor;
            colorChangeAnimation.Duration = TimeSpan.FromMilliseconds(miliseconds);

            PropertyPath colorTargetPath = new PropertyPath("(Polygon.Fill).(SolidColorBrush.Color)");
            Storyboard CellBackgroundChangeStory = new Storyboard();
            Storyboard.SetTarget(colorChangeAnimation, _polygon);
            Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
            CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
            CellBackgroundChangeStory.Begin();
        }
    }
}
