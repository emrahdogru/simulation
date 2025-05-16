using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using Simulation.Domain.Items.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain
{
    public class Receipt(uint workForce, HashSet<Building> buildings, ItemCollection<IItem> ingredients, IEnumerable<IItem> sideProducts)
    {
        public uint WorkForce => workForce;

        /// <summary>
        /// Üretilebileceği binalar
        /// </summary>
        public HashSet<Building> Buildings => buildings;

        /// <summary>
        /// Üretimi için gerekli olan hammaddeler ve miktarları
        /// </summary>
        public ItemCollection<IItem> Ingredients => ingredients;

        /// <summary>
        /// Yan mamüller (Item:WorkForce)
        /// </summary>
        public IEnumerable<IItem> SideProducts => sideProducts;
    }
}
