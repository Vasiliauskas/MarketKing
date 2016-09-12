namespace MarketKing.Game
{
    public class Player
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int OwnedTiles { get; set; }
        public int ResourcePool { get; set; }
        public Player(IStrategy strategy, int id)
        {
            Name = strategy.Name;
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}