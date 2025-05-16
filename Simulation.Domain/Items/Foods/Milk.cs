using Simulation.Domain.Enums;
using System.Collections.ObjectModel;

namespace Simulation.Domain.Items.Foods
{
    public class Milk : Food
    {
        public override ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new ReadOnlyDictionary<NutrientType, float>(new Dictionary<NutrientType, float>() { { NutrientType.Protein, 1f } });

        public override string Name => "Milk";

        public override Receipt Receipt => new(1000, [], [], []);
    }
}
