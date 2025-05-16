using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Buildings
{
    public class OliveGrove : Building
    {
        public override string Name => "Olive Grove";
        public override Receipt Receipt => new(1500, [], new() { [Item.Wood] = 20 }, []);
        public override ProducerType ProducerType => ProducerType.Continuous;
    }
}
