using Simulation.Domain.Enums;
using Simulation.Domain.Exceptions;
using Simulation.Domain.Items.Abstractions;

namespace Simulation.Domain.Items.Buildings
{
    public abstract class Building : Item
    {
        private IEnumerable<IItem>? _manufacturableItems = null;

        public virtual int MaxWorkerCount { get; } = 8;
        public virtual int HousingCapacity { get; } = 0;
        public virtual Dictionary<CapacityType, int> Capacities { get; } = [];
        public virtual float DeathRisk { get; } = 0.1f;
        public abstract ProducerType ProducerType { get; }

        /// <summary>
        /// Returns a list of items that can be manufactured in this building.
        /// </summary>
        public IEnumerable<IItem> ManufacturableItems
        {
            get
            {
                _manufacturableItems ??= IItem.GetItemList().Where(x => x.Receipt.Buildings.Contains(this)).ToArray();
                return _manufacturableItems;
            }
        }
    }
}
