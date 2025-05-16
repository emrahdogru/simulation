using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulation.Domain.BuildingContainer;

namespace Simulation.Domain.Producers
{
    public class QueueProducer(BuildingContainer container) : Producer(container)
    {
        public IEnumerable<QueueItem> Queue { get; set; } = [];

        public override void ConsumeWorkForce()
        {
            container.Exception = null;
            if (!Queue.Any())
                return;

            var queueItem = Queue.FirstOrDefault(x => x.IsProducing);
            if (queueItem == null)
            {
                foreach (var qi in Queue)
                {
                    try
                    {
                        container.Section.Items -= qi.Item.Receipt.Ingredients;
                        queueItem = qi;
                        queueItem.IsProducing = true;
                        container.Exception = null;
                    }
                    catch (NotFiniteNumberException ex)
                    {
                        container.Exception ??= ex;
                    }
                }
            }

            if (container.WorkForce > queueItem?.Item.Receipt.WorkForce)
            {
                container.Section.Items.Add(queueItem.Item, 1);
                queueItem.Amount -= 1;
                if (queueItem.Amount <= 0)
                    Queue = Queue.Where(x => x != queueItem).ToArray();
                else
                    queueItem.IsProducing = false;
            }
        }
    }
}
