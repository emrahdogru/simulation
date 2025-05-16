using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Foods
{
    public class Chicken : Food
    {
        public override ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new(new Dictionary<NutrientType, float>() { { NutrientType.Protein, 50f }, { NutrientType.Fat, 10f } });

        public override string Name => "Chicken";

        public override Receipt Receipt => new(1000, [Item.PoultryFarm], [], [Item.Egg] );
    }
}
