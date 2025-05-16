using Simulation.Domain.Enums;
using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using Simulation.Domain.Items.Buildings;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Simulation.Domain
{
    public class Section
    {
        private Dictionary<CapacityType, CapacityInfo>? _capacities = null;
        private int? _citizenCapacity = null;
        private ItemCollection<IItem>? _absoluteCapacities = new ItemCollection<IItem>();

        public Section(Game game, string name, Point location)
        {
            Game = game;
            Name = name;
            Location = location;
            Buildings = [];
            Items = [];
            Citizens = [];
        }

        public Section(Game game, SectionTemplate template)
        {
            ArgumentNullException.ThrowIfNull(game, nameof(game));
            ArgumentNullException.ThrowIfNull(template, nameof(template));

            Game = game;
            Name = template.Name;
            Location = template.Location;
            Buildings = [];
            Items = [];
            Citizens = [.. Enumerable.Range(0, template.CitizenCount).Select(x => Citizen.Random(this, 15, 40))];

            Items += template.Resouces;

        }

        public Game Game { get; }
        public string Name { get; }
        public Point Location { get; set; }
        public bool IsOccupied { get; set; } = false;

        public IEnumerable<BuildingContainer> Buildings { get; private set; }
        public ItemCollection<IItem> Items { get; internal set; }
        public HashSet<Citizen> Citizens { get; private set; }

        /// <summary>
        /// AbsoluteCapacity is the capacity of an item that comes from the buildings producing it and can
        /// only be used for that specific item. Once this capacity is full, any additional amounts are
        /// transferred to the shared storage (Warehouse, etc.).
        /// </summary>
        public int GetAbsolteCapacity(IItem item)
        {
            return Buildings.Where(x => x.Building.ManufacturableItems.Contains(item)).Sum(x => x.Building.Capacities.GetValueOrDefault(item.CapacityType));
        }

        public Dictionary<CapacityType, CapacityInfo> Capacities
        {
            get
            {
                if (_capacities == null)
                {
                    var capacity = Buildings.Where(x => x.IsCompleted).SelectMany(x => x.Building.Capacities).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Sum(y => y.Value));
                    var used = this.Items.GroupBy(x => x.Key.CapacityType).ToDictionary(x => x.Key, x => Math.Max(0,  x.Sum(y => y.Key.Weight * y.Value) - x.Sum(y => GetAbsolteCapacity(y.Key))));
                    _capacities = capacity.ToDictionary(x => x.Key, x => new CapacityInfo { Capacity = x.Value, Used = used.GetValueOrDefault(x.Key, 0) });
                }
                return _capacities;
            }
        }

        public int CitizenCapacity
        {
            get
            {
                _citizenCapacity ??= Buildings.Where(x => x.IsCompleted).Sum(x => x.Building.HousingCapacity);
                return _citizenCapacity.Value;
            }
        }

        public int UnemployedCitizen => Citizens.Where(x => x.WorkPlace == null).Count();

        public int FreeBed => CitizenCapacity - Citizens.Count;

        public Dictionary<CapacityType, bool> OutOfCapacityWarning { get; set; } = new();

        internal void RefreshCapacities()
        {
            _capacities = null;
            _citizenCapacity = null;
            _absoluteCapacities = null;
        }

        public BuildingContainer Build(Building building)
        {
            ArgumentNullException.ThrowIfNull(building, nameof(building));
            try
            {
                Items -= building.Receipt.Ingredients;
                var container = new BuildingContainer(this, building);
                Buildings = Buildings.Append(container).ToArray();
                return container;
            }
            catch (Exception ex)
            {
                Game.AddException(ex);
                throw;
            }
        }

        public void Process()
        {
            RefreshCapacities();

            foreach (var b in Buildings)
            {
                b.Process();
            }

            foreach(var c in Citizens)
            {
                c.Process();
            }

            if(Game.Time.Ticks % 10 == 0 && FreeBed > 0)
            {
                var citizen = Citizen.Random(this, 18, 25);
                Citizens.Add(citizen);
            }
        }

        internal bool HasCapacityForItem(IItem item, float amount = 1)
        {
            bool result = Capacities.GetValueOrDefault(item.CapacityType)?.Free >= item.Weight * amount;
            if(!result)
            {
                var existAmount = Items.GetValueOrDefault(item);
                var absolteCapacity = Buildings.Where(x => x.Building.ManufacturableItems.Contains(item)).Sum(x => x.Building.Capacities.GetValueOrDefault(item.CapacityType));
                if (existAmount * item.Weight < absolteCapacity)
                    result = true;
            }

            return result;
        }

        public class CapacityInfo
        {
            public float Capacity { get; internal set; }
            public float Used { get; internal set; }
            public float Free => Capacity - Used;
        }
    }
}
