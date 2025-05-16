namespace Simulation.Domain.Items
{
    public class Iron : Item
    {
        public override string Name => "Iron";

        public override Receipt Receipt => new(30, [Item.IronMine], [], []);
    }
}
