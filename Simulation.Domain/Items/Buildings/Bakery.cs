using Simulation.Domain.Enums;
using Simulation.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Bakery : Building
    {
        public override string Name => "Bakery";

        public override Receipt Receipt => new(30, [], new() { [Item.Wood] = 10, [Item.Stone] = 30 }, []);

        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Grain] = 50, [CapacityType.Water] = 20, [CapacityType.Food] = 20 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
