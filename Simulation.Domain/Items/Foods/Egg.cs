using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Foods
{
    public class Egg : Food
    {
        public override ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new ReadOnlyDictionary<NutrientType, float>(new Dictionary<NutrientType, float>() { { NutrientType.Protein, 5f } });

        public override string Name => "Egg";

        public override Receipt Receipt => new(2000, [], [], []);
    }
}
