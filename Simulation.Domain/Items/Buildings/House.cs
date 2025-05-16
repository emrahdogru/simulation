using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class House : Building
    {
        public override string Name => "House";
        public override int MaxWorkerCount => 0;
        public override int HousingCapacity => 8;
        public override Receipt Receipt => new (120,[], new() { [Item.Wood] = 10 , [Item.Stone] = 5 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Water] = 4, [CapacityType.Warehouse] = 10 };
        public override ProducerType ProducerType => ProducerType.None;
    }
}
