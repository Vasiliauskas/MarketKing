using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MarketKing.Game
{
    public class Render
    {
        private readonly Canvas _canvas;
        private readonly Dispatcher _uiDispatcher;
        private readonly ZoomScrollViewer _zoom;
        private readonly Dictionary<Cell, Hexagon> _hexagonLookup;
        public Render(Canvas canvas, ZoomScrollViewer zoom, Dispatcher uiDispatcher)
        {
            _canvas = canvas;
            _uiDispatcher = uiDispatcher;
            _zoom = zoom;
            _hexagonLookup = new Dictionary<Cell, Hexagon>();
        }

        public Task DrawBoardAsync(Board board)
        {
            return Task.Run(async () =>
            {
                await _uiDispatcher.InvokeAsync(() =>
                 {
                     _canvas.Height = board.Cells.Keys.Max(k => (k.Y + 2) * 2 * GameConfig.StepSize);
                     _canvas.Width = board.Cells.Keys.Max(k => (k.X + 2) * 2 * GameConfig.StepSize);
                 });
                foreach (var cell in board.Cells)
                    await DrawHexagon(cell.Value, board.StartLocations.Contains(cell.Key));
            });
        }

        private Task DrawHexagon(Cell cell, bool isStartLocation)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            if (GameConfig.ThrottleRenderMiliseconds > 0)
                Thread.Sleep(GameConfig.ThrottleRenderMiliseconds);

            _uiDispatcher.InvokeAsync(() =>
                {
                    bool isOdd = cell.CenterLocation.X % 2 == 0;
                    var hexColor = Colors.White;
                    if (isStartLocation)
                        hexColor = Colors.Red;

                    var hex = new Hexagon(hexColor);
                    _hexagonLookup.Add(cell, hex);
                    _canvas.Children.Add(hex.Control);
                    double y = (cell.CenterLocation.Y) * GameConfig.StepSize * GameConfig.HeightRatio - (isOdd ? GameConfig.StepSize * GameConfig.HeightRatio / 2 : 0);
                    double x = (cell.CenterLocation.X) * GameConfig.StepSize * 2;
                    Canvas.SetTop(hex.Control, y);
                    Canvas.SetLeft(hex.Control, x);
                    while (x + GameConfig.StepSize * 3 > (_zoom.ActualWidth / _zoom.Zoom))
                    {
                        _zoom.Zoom -= 0.01;
                        Thread.Sleep(1);
                    }

                    tcs.SetResult(null);
                });
            return tcs.Task;
        }
    }
}
