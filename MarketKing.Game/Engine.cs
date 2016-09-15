namespace MarketKing.Game
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Windows.Media;
    using System.Threading;

    public class Engine
    {
        private readonly Dictionary<Player, IStrategy> _players;
        private readonly Render _render;
        private readonly Board _board;
        public Engine(IStrategy[] strategies, Render render)
        {
            _players = new Dictionary<Player, IStrategy>();
            _render = render;
            _board = new Board(strategies.Length);

            for (int i = 0; i < strategies.Length; i++)
            {
                var uniqueColor = (Color)ColorConverter.ConvertFromString(UniqueColorProvider.GetUniqueColor(i));
                var player = new Player(strategies[i], i, uniqueColor);
                _players.Add(player, strategies[i]);
            }
        }

        public async Task RunStartSequence()
        {
            await Task.Run(async () => await _render.DrawBoardAsync(_board));
            SetupStartLocations();
            await RunGame();
        }

        private void SetupStartLocations()
        {
            int i = 0;
            foreach (var player in _players.Keys)
            {
                AssignCellToPlayer(_board.Cells[_board.StartLocations[i]], player);
                SetCellResource(_board.Cells[_board.StartLocations[i]], GameConfig.StartingResource);
                i++;
            }
        }

        private Task RunGame()
        {
            return Task.Run(() =>
            {
                while (!DoWeHaveAWinner())
                {
                    TakeTurns();
                    IncrementResources();
                    Thread.Sleep(50);
                }
            });
        }

        private void TakeTurns()
        {
            foreach (var player in _players.ToList())
            {
                var playerCells = _board.Cells.Values.Where(c => c.OwnedById.HasValue && c.OwnedById.Value.Equals(player.Key.Id))
                    .Select(c => ToMyCell(c));
                if (!playerCells.Any())
                {
                    _players.Remove(player.Key);
                    continue;
                }
                // maybe async and then timeout if no response after 500 ms
                var transaction = player.Value.Turn(playerCells.ToArray());

                var targetCell = transaction.TargetBlock != null ? _board.Cells[transaction.TargetBlock.CenterLocation] : null;
                var sourceCell = transaction.MyBlock != null ? _board.Cells[transaction.MyBlock.CenterLocation] : null;
                if (sourceCell != null && targetCell != null)
                {
                    var resourceToTransfer =
                        sourceCell.Resources >= transaction.AmmountToTransfer ?
                        transaction.AmmountToTransfer : sourceCell.Resources;

                    SetCellResource(sourceCell, sourceCell.Resources -= resourceToTransfer);

                    if (resourceToTransfer >= targetCell.Resources
                        && ((targetCell.OwnedById.HasValue
                        && targetCell.OwnedById.Value != player.Key.Id) || !targetCell.OwnedById.HasValue))
                    {
                        AssignCellToPlayer(targetCell, player.Key);
                        SetCellResource(targetCell, resourceToTransfer - targetCell.Resources);
                    }
                    else
                    {
                        SetCellResource(targetCell, targetCell.Resources += resourceToTransfer);
                    }
                }
            }
        }

        private void IncrementResources()
        {
            var ownedCells = _board.Cells.Values.Where(v => v.OwnedById.HasValue);
            foreach (var cell in ownedCells)
            {
                var resources = cell.Resources <= 99 ? cell.Resources + GameConfig.TurnGrowth : cell.Resources;
                SetCellResource(cell, resources);
            }
        }

        private bool DoWeHaveAWinner() =>
            _board.Cells.Values
            .Where(c => c.OwnedById.HasValue)
            .Select(c => c.OwnedById.Value).Distinct()
            .Count() <= 1;

        private void AssignCellToPlayer(Cell cell, Player player)
        {
            cell.OwnedById = player.Id;
            _render.SetColor(cell, player.Color);
        }

        private void SetCellResource(Cell cell, int resource)
        {
            cell.Resources = resource;
            _render.ChangeResourceValue(cell);
        }

        private MyCell ToMyCell(Cell c)
        {
            return new MyCell(c);
        }
    }
}
