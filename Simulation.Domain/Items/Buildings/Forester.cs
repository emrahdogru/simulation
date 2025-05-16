using Simulation.Domain.Enums;

namespace Simulation.Domain.Items.Buildings
{
    public class Forester : Building
    {
        public override string Name => "Forester";

        public override Receipt Receipt => new(30, [], new() { [Item.Wood] = 10 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Warehouse] = 50 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
