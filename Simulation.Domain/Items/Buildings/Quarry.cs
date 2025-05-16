using Simulation.Domain.Enums;

namespace Simulation.Domain.Items.Buildings
{
    public class Quarry : Building
    {
        public override string Name => "Quarry";
        public override Receipt Receipt => new(300, [], new() { [Item.Wood] = 20 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Warehouse] = 20 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
