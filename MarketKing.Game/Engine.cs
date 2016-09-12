using System;
namespace MarketKing.Game
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        private async Task RunGame()
        {

        }
    }
}
