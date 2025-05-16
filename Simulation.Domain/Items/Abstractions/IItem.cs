using Simulation.Domain.Enums;
using Simulation.Domain.Items.Buildings;
using System.Reflection;

namespace Simulation.Domain.Items.Abstractions
{
    public interface IItem
    {
        CapacityType CapacityType { get; }
        string Key { get; }
        string Name { get; }
        Receipt Receipt { get; }
        float Weight { get; }

        private static IEnumerable<IItem>? _items = null;

        public static IEnumerable<IItem> GetItemList()
        {
            _items ??= typeof(Item)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(p => p.PropertyType.IsAssignableTo(typeof(IItem)))
                    .Select(p => (IItem)p.GetValue(null)!)
                    .Where(p => p != null)
                    .ToArray();

            return _items;
        }

        public static IItem? FindByKey(string key)
        {
            return GetItemList().FirstOrDefault(x => x.Key == key);
        }
    }
}