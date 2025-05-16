namespace Simulation.Domain.Items
{
    public class Coal : Item
    {
        public override string Name => "Coal";
        public override Receipt Receipt => new(10, [Item.CoalMine], [], []);
    }
}
