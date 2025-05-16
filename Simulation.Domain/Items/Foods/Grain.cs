using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Foods
{
    public class Grain : Food
    {
        public override string Name => "Grain";
        public override ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new ReadOnlyDictionary<NutrientType, float>(new Dictionary<NutrientType, float>() { { NutrientType.Carbohydrate, 2f } });
        public override Receipt Receipt => new(2, [Item.Field], [], []);
        public override CapacityType CapacityType => CapacityType.Grain;
    }
}
