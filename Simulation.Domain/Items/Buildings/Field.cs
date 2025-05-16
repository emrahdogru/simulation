using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Field : Building
    {
        public override string Name => "Field";
        public override Receipt Receipt => new (10, [], new() { [Item.Wood] = 5 }, []);
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Grain] = 50, [CapacityType.Water] = 20 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
