using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class Blacksmith : Building
    {
        public override string Name => "Blacksmity";
        public override Receipt Receipt => new Receipt(50, [], new() { [Item.Iron] = 5, [Item.Stone] = 2 }, []);
        public override ProducerType ProducerType => ProducerType.Queue;
    }
}
