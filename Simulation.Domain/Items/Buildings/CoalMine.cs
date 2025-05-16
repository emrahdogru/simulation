using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class CoalMine : Building
    {
        public override string Name => "Coal Mine";

        public override Receipt Receipt => new(400, [], new(){  [Item.Wood] = 10, [Item.Stone] = 5  }, []);

        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Warehouse] = 20 };
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
