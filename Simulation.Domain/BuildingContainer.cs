using Simulation.Domain.Items.Abstractions;
using Simulation.Domain.Items.Buildings;
using Simulation.Domain.Producers;

namespace Simulation.Domain
{
    public class BuildingContainer
    {
        private int _workerCount = 0;
        private List<Citizen> workers = [];

        public BuildingContainer(Section section, Building building)
        {
            ArgumentNullException.ThrowIfNull(building, nameof(building));
            ArgumentNullException.ThrowIfNull(section, nameof(section));

            Building = building;
            Section = section;
            WorkerCount = MaxWorkerCount;
            ConstructionTime = section.Game.Time;
        }
        public Section Section { get; }
        public Building Building { get; }
        public Date ConstructionTime { get; }
        public float CompletedDuration { get; private set; }
        public bool IsCompleted => CompletedDuration >= Building.Receipt.WorkForce;
        public int MaxWorkerCount => IsCompleted ? Building.MaxWorkerCount : 5;
        public Exception? Exception { get; internal set; }
        public int WorkerCount
        {
            get => _workerCount;
            set
            {
                if (value > MaxWorkerCount)
                    _workerCount = MaxWorkerCount;
                else
                    _workerCount = value;
            }
        }

        public IEnumerable<Citizen> Workers => workers;

        internal float WorkForce { get; set; }
        public Producer? Producer { get; private set; } = null;


        private void HireWorkers()
        {
            if (workers.Count() < WorkerCount && Section.Citizens.Count > 0)
            {
                var citizen = Section.Citizens.Where(x => x.WorkPlace == null).FirstOrDefault();
                if (citizen != null)
                {
                    workers.Add(citizen);
                    citizen.WorkPlace = this;
                }
            }
            else if (workers.Count() > WorkerCount)
            { 
                var citizen = workers.FirstOrDefault();
                if (citizen != null)
                {
                    workers.Remove(citizen);
                    citizen.WorkPlace = null;
                }
            }
        }

        public void Process()
        {
            HireWorkers();
            if (!IsCompleted)
            {
                CompletedDuration += Workers.Sum(x => 1) * 1f;
                if(IsCompleted)
                {
                    foreach(var w in this.Workers)
                        w.WorkPlace = null;
                    workers = [];
                    WorkerCount = Building.MaxWorkerCount;
                    Producer = Producer.Create(this);
                }
            }
            else
            {
                WorkForce += Workers.Sum(x => x.Efficiency);
                Producer?.ConsumeWorkForce();
            }
        }

        public override string ToString()
        {
            return Building?.Name + (IsCompleted ? "" : " Construction");
        }

        public class QueueItem
        {
            public QueueItem(IItem item, float amount)
            {
                Item = item;
                Amount = amount;
                IsProducing = false;
            }

            public IItem Item { get; set; }
            public float Amount { get; set; }
            public bool IsProducing { get; internal set; }
        }
    }
}
