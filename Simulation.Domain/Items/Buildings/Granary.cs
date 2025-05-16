using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Granary : Building
    {
        public override string Name => "Granary";
        public override int MaxWorkerCount => 0;
        public override Receipt Receipt => new(250, [], new() { [Item.Wood] = 50, [Item.Stone] = 100, [Item.Iron] = 20 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Grain] = 500 };
        public override ProducerType ProducerType => ProducerType.None;
    }
}
