using Simulation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Items.Abstractions
{
    public interface IFood : IItem
    {
        public ReadOnlyDictionary<NutrientType, float> Nutrients { get; }
    }
}
