using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Cistern : Building
    {
        public override string Name => "Cistern";
        public override int MaxWorkerCount => 0;
        public override Receipt Receipt => new Receipt(200, [], new() { [Item.Wood] = 10, [Item.Stone] = 100 }, []);
        public override Dictionary<CapacityType, int> Capacities { get; } = new() { [CapacityType.Water] = 500 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
