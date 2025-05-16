using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain
{
    public class SectionTemplate
    {
        internal SectionTemplate(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        public string Name { get; internal set; }
        public Point Location { get; internal set; }

        public ItemCollection<IItem> Resouces { get; internal set; } = [];
        public int CitizenCount { get; internal set; } = 10;
    }
}
