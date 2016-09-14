using System.Windows.Media;

namespace MarketKing.Game
{
    public class Player
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
    }
}