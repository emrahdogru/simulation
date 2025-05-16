using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class WaterWell : Building
    {
        public override string Name => "Water Well";
        public override Receipt Receipt => new(100, [], new() { [Item.Wood] = 5, [Item.Stone] = 10 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Water] = 50 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
