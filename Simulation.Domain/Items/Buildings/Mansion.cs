using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Mansion : Building
    {
        public override string Name => "Mansion";
        public override int MaxWorkerCount => 0;
        public override int HousingCapacity => 24;
        public override Receipt Receipt => new(1000, [], new() { [Item.Wood] = 100, [Item.Stone] = 50 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Water] = 20, [CapacityType.Warehouse] = 10 };
        public override ProducerType ProducerType => ProducerType.None;
    }
}
