using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class PoultryFarm : Building
    {
        public override string Name => "Poultry Farm";
        public override Receipt Receipt => new(250, [], new() { [Item.Wood] = 20 }, []);
        public override ProducerType ProducerType => ProducerType.Breeding;
        public override Dictionary<CapacityType, int> Capacities => new() { [CapacityType.Grain] = 50, [CapacityType.Water] = 20, [CapacityType.Food] = 20 };
    }
}
