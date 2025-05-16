using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Producers
{
    public class BreedingProducer : Producer
    {
        private float amount = 1;
        private ItemCollection<IItem> sideProducts = [];

        public BreedingProducer(BuildingContainer container) : base(container)
        {
            ProducingItem = container.Building.ManufacturableItems.FirstOrDefault() ?? throw new ArgumentException($"The manufacturable object for the {container.Building.Name} is not defined.");
        }

        public IItem ProducingItem { get; set; }

        public int Limit { get; } = 50;

        public int Amount { get; private set; } = 0;

        

        public override void ConsumeWorkForce()
        {
            if (!ProductionHistory.ContainsKey(container.Section.Game.Time.OnlyDate))
                ProductionHistory[container.Section.Game.Time.OnlyDate] = [];

            amount = Math.Min(Limit, amount + (Amount * (container.WorkForce / ProducingItem.Receipt.WorkForce)));
            foreach(var sp in ProducingItem.Receipt.SideProducts)
            {
                sideProducts.Add(sp, (container.WorkForce / sp.Receipt.WorkForce) * Amount);
            }
            container.WorkForce = 0;

            if(amount >= 1)
            {
                ProductionHistory[container.Section.Game.Time.OnlyDate].Add(ProducingItem, (int)amount);
                Amount += (int)amount;
                amount -= (int)amount;

                if(Amount > Limit)
                {
                    container.Section.Items.Add(ProducingItem, Amount - Limit);
                    Amount = Limit;
                }
            }
            

            // Move completed side products to the section
            foreach (var sp in sideProducts)
            {
                if (sp.Value >= 1)
                {
                    int spAmount = (int)sp.Value;
                    sideProducts[sp.Key] -= spAmount;
                    container.Section.Items.Add(sp.Key, spAmount);

                    ProductionHistory[container.Section.Game.Time.OnlyDate].Add(sp.Key, spAmount);
                }
            }
        }
    }
}
