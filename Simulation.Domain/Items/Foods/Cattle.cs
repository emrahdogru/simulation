using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Foods
{
    public class Cattle : Food
    {
        public override ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new(new Dictionary<NutrientType, float>() { { NutrientType.Protein, 500f }, { NutrientType.Fat, 100f } });

        public override string Name => "Cattle";

        public override Receipt Receipt => new(10000, [Item.CattleFarm], [], [Item.Milk]);
    }
}
