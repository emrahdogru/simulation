using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Foods
{
    public class Meat : Food
    {
        public override ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new ReadOnlyDictionary<NutrientType, float>(new Dictionary<NutrientType, float>() { { NutrientType.Protein, 30f } });

        public override string Name => "Meat";

        public override Receipt Receipt => throw new NotImplementedException();
    }
}
