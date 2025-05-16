using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Warehouse : Building
    {
        public override string Name => "Warehouse";
        public override int MaxWorkerCount => 0;
        public override Receipt Receipt { get; } = new(100, [], new() { [Item.Wood] = 20, [Item.Stone] = 50 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Warehouse] = 500 };
        public override ProducerType ProducerType => ProducerType.None;
    }
}
