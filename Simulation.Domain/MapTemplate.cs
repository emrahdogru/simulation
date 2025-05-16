using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain
{
    public class MapTemplate
    {
        private static IEnumerable<MapTemplate>? _mapTemplates = null;

        protected MapTemplate(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        private static readonly ItemCollection<IItem> FirstSectionResources = new()
        {
            [Item.Iron] = 100,
            [Item.Wood] = 200,
            [Item.Coal] = 50,
            [Item.Stone] = 200,
            [Item.Grain] = 500,
            [Item.Water] = 500,
            [Item.Bread] = 100,
            [Item.Egg] = 100
        };

        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public IEnumerable<SectionTemplate> Sections { get; private set; } = [];

        public static MapTemplate Template01 => new("Template 01")
        {
            Description = "Proin id tincidunt ante. Duis enim risus, ornare eget placerat in, gravida non lacus. Etiam viverra, leo et consectetur vulputate.",
            Sections =
            [
                new SectionTemplate("Section 01") { Resouces = FirstSectionResources, CitizenCount = 10 },
            ]
        };

        public static MapTemplate Template02 => new("Template 02")
        {
            Description = "Integer sollicitudin leo gravida orci rutrum, ut aliquam turpis dapibus. Ut fermentum placerat turpis, id ultrices libero pharetra et. Duis.",
            Sections =
            [
                new SectionTemplate("Section 01") { Resouces = FirstSectionResources },
                new SectionTemplate("Section 02"),
                new SectionTemplate("Section 03"),
            ]
        };

        public static MapTemplate Template03 => new("Template 03")
        {
            Description = "Donec commodo enim non fermentum ultrices. Aliquam a metus eu odio sagittis consectetur vitae in justo. Donec scelerisque, ante vitae.",
            Sections =
            [
                new SectionTemplate("Section 01") { Resouces = FirstSectionResources },
                new SectionTemplate("Section 02"),
                new SectionTemplate("Section 03"),
            ]
        };

        public static MapTemplate Template04 => new("Template 04")
        {
            Description = "Ut et elit neque. Vestibulum vitae odio vitae nisi semper pharetra eu eget nisl. Donec aliquet leo et efficitur fermentum.",
            Sections =
            [
                new SectionTemplate("Section 01") { Resouces = FirstSectionResources },
                new SectionTemplate("Section 02"),
                new SectionTemplate("Section 03"),
            ]
        };

        public static IEnumerable<MapTemplate> MapTemplates
        {
            get
            {
                _mapTemplates ??= typeof(MapTemplate)
                        .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                        .Where(p => p.PropertyType.IsAssignableTo(typeof(MapTemplate)))
                        .Select(p => (MapTemplate)p.GetValue(null)!)
                        .Where(p => p != null)
                        .ToArray();

                return _mapTemplates;
            }
        }
    }
}
