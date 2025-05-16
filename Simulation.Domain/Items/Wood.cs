namespace Simulation.Domain.Items
{
    public class Wood : Item
    {
        public override string Name => "Wood";

        public override Receipt Receipt => new(10, [Item.Forester], [], []);
    }
}
