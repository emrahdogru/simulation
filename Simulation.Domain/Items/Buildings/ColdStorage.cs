using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class ColdStorage : Building
    {
        public override string Name => "Cold Storage";
        public override int MaxWorkerCount => 0;
        public override Receipt Receipt { get; } = new(100, [], new() { [Item.Wood] = 20, [Item.Stone] = 50, [Item.Iron] = 10 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Food] = 500 };
        public override ProducerType ProducerType => ProducerType.None;
    }
}
