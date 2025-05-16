using Simulation.Domain.Enums;
using Simulation.Domain.Items.Abstractions;
using Simulation.Domain.Items.Buildings;
using Simulation.Domain.Items.Foods;
using System.Reflection;

namespace Simulation.Domain.Items
{
    public abstract class Item : IItem
    {
        public string Key => this.GetType().Name;
        public abstract string Name { get; }
        public virtual CapacityType CapacityType { get; } = CapacityType.Warehouse;
        public virtual float Weight { get; } = 1f;
        public abstract Receipt Receipt { get; }


        public static Water Water { get; } = new();
        public static Coal Coal { get; } = new();
        public static Iron Iron { get; } = new();
        public static Wood Wood { get; } = new();
        public static Stone Stone { get; } = new();
        public static Grain Grain { get; } = new();

        public static Chicken Chicken { get; } = new();

        public static House House { get; } = new();
        public static Mansion Mansion { get; } = new();
        public static Field Field { get; } = new();
        public static Quarry Quarry { get; } = new();
        public static CoalMine CoalMine { get; } = new();
        public static Forester Forester { get; } = new();
        public static IronMine IronMine { get; } = new();
        public static Granary Granary { get; } = new();
        public static Bakery Bakery { get; } = new();
        public static Bread Bread { get; } = new();
        public static WaterWell WaterWell { get; } = new();
        public static Cistern Cistern { get; } = new();
        public static Warehouse Warehouse { get; } = new();
        public static ColdStorage ColdStorage { get; } = new();
        public static PoultryFarm PoultryFarm { get; } = new();
        public static CattleFarm CattleFarm { get; } = new();
        public static Egg Egg { get; } = new();
        public static Milk Milk { get; } = new();
        public static Cattle Cattle { get; } = new();
    }
}
