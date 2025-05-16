using Simulation.Domain.Enums;
using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items
{
    public class Water : Item, IFood
    {
        public override string Name => "Water";
        public override Receipt Receipt => new(5, [Item.WaterWell], [], []);
        public override CapacityType CapacityType => CapacityType.Water;

        public ReadOnlyDictionary<NutrientType, float> Nutrients { get; } = new ReadOnlyDictionary<NutrientType, float>(new Dictionary<NutrientType, float>() { { NutrientType.Water, 7f } });
    }
}
