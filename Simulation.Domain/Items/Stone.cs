namespace Simulation.Domain.Items
{
    public class Stone : Item
    {
        public override string Name => "Stone";

        public override Receipt Receipt => new(15, [Item.Quarry], [], []);
    }
}
