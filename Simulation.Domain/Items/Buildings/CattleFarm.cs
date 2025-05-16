using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class CattleFarm : Building
    {
        public override string Name => "Cattle Farm";
        public override Receipt Receipt => new(200, [], new() { [Item.Wood] = 10, [Item.Stone] = 5 }, []);
        public override ProducerType ProducerType => ProducerType.Breeding;
    }
}
