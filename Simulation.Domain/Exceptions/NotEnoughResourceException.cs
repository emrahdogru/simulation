using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Exceptions
{
    /// <summary>
    /// When there is not enough resource...
    /// </summary>
    public class NotEnoughResourceException(IItem item, float required, float exist) : Exception
    {
        public IItem Item { get; } = item;
        public float Required { get; } = required;
        public float Exist { get; } = exist;

        public override string Message => $"Not enough {Item.Name}. Required: {Required} Available: {Exist}";

        public static void ThrowIfNotEnough(IItem item, float required, float exist)
        {
            if (required > exist)
                throw new NotEnoughResourceException(item, required, exist);
        }
    }
}
