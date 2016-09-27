namespace MarketKing.Game
{
    using System;
    using System.Windows.Media;

    public class Player : IObservable<Player>
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int OwnedTiles { get; set; }
        public int ResourcePool { get; set; }
        public Color Color { get; set; }
        public Player(IStrategy strategy, int id, Color color)
        {
            Name = strategy.Name;
            Id = id;
            Color = color;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public IDisposable Subscribe(IObserver<Player> observer)
        {
            throw new NotImplementedException();
        }
    }
}