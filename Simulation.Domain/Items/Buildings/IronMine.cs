using Simulation.Domain.Enums;

namespace Simulation.Domain.Items.Buildings
{
    public class IronMine : Building
    {
        public override string Name => "Iron Mine";
        public override Receipt Receipt => new(300, [], new() { [Item.Wood] = 50, [Item.Stone] = 15 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Warehouse] = 20 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
