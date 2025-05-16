using Simulation.Domain.Exceptions;
using Simulation.Domain.Items.Abstractions;

namespace Simulation.Domain.Producers
{
    public class ContinuousProducer : Producer
    {
        public ContinuousProducer(BuildingContainer container): base(container)
        {
            ProducingItem = container.Building.ManufacturableItems.FirstOrDefault() ?? throw new ArgumentException($"The manufacturable object for the {container.Building.Name} is not defined.");
        }

        public IItem? ProducingItem { get; set; }

        public override void ConsumeWorkForce()
        {
            if (ProducingItem != null && container.WorkForce > ProducingItem.Receipt.WorkForce)
            {
                if (!ProductionHistory.ContainsKey(container.Section.Game.Time.OnlyDate))
                    ProductionHistory[container.Section.Game.Time.OnlyDate] = [];

                try
                {
                    container.WorkForce -= ProducingItem.Receipt.WorkForce;

                    if (container.Section.HasCapacityForItem(ProducingItem))
                    {
                        if (ProducingItem.Receipt.Ingredients.Any())
                            container.Section.Items -= ProducingItem.Receipt.Ingredients;
                        container.Section.Items.Add(ProducingItem, 1);
                        ProductionHistory[container.Section.Game.Time.OnlyDate].Add(ProducingItem, 1);
                    }
                    else
                    {
                        container.Section.OutOfCapacityWarning[ProducingItem.CapacityType] = true;
                    }
                    container.Exception = null;
                }
                catch (NotEnoughResourceException ex)
                {
                    container.Exception = ex;
                }
            }
        }
    }
}
