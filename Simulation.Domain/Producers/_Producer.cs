using Simulation.Domain.Enums;
using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain.Producers
{
    public abstract class Producer
    {
        protected BuildingContainer container;

        public Producer(BuildingContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public Dictionary<Date, ItemCollection<IItem>> ProductionHistory { get; private set; } = [];

        public abstract void ConsumeWorkForce();

        public static Producer? Create(BuildingContainer container)
        {
            return container.Building.ProducerType switch
            {
                ProducerType.Breeding => new BreedingProducer(container),
                ProducerType.Queue => new QueueProducer(container),
                ProducerType.Continuous => new ContinuousProducer(container),
                _ => null
            };
        }
    }
}
