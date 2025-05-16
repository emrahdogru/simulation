using Simulation.Domain.Enums;
using Simulation.Domain.Items.Abstractions;
using System.Collections.ObjectModel;

namespace Simulation.Domain.Items.Foods
{
    public abstract class Food : Item, IFood
    {
        public abstract ReadOnlyDictionary<NutrientType, float> Nutrients { get; }

        public override CapacityType CapacityType => CapacityType.Food;
    }
}
