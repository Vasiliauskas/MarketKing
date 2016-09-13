namespace MarketKing.Game
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

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
                _players.Add(new Player(strategies[i], i), strategies[i]);
                _board.Cells[_board.StartLocations[i]].OwnedById = i;
                _board.Cells[_board.StartLocations[i]].Resources = GameConfig.StartingResource;
            }
        }

        public async Task RunStartSequence()
        {
            await Task.Run(async () => await _render.DrawBoardAsync(_board));
            await RunGame();
        }

        private Task RunGame()
        {
            return Task.Run(() =>
            {
                while (CheckWinner())
                {
                    foreach (var player in _players)
                    {
                        var playerCells = _board.Cells.Values.Where(c => c.OwnedById.HasValue && c.OwnedById.Value.Equals(player.Key.Id))
                            .Select(c => new MyCell(c));
                        var transaction = player.Value.Turn(playerCells.ToArray());
                        var targetCell = transaction.TargetBlock;
                        var sourceCell = transaction.MyBlock;
                        if (sourceCell != null && targetCell != null)
                        {
                            var resourceToTransfer =
                                sourceCell.Resources >= transaction.AmmountToTransfer ?
                                transaction.AmmountToTransfer : sourceCell.Resources;
                            sourceCell.Resources -= resourceToTransfer;
                            targetCell.Resources += resourceToTransfer;
                            if (targetCell.Resources > 99)
                                targetCell.Resources = 99;
                        }
                    }
                }
            });
        }

        private bool CheckWinner() =>
            _board.Cells.Values
            .Where(c => c.OwnedById.HasValue)
            .Select(c => c.OwnedById.Value).Distinct()
            .Count() <= 1;
    }
}
