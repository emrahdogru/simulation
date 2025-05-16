using System.Collections.ObjectModel;

namespace Simulation.Domain
{
    public class Game : IDisposable
    {
        private bool disposedValue;

        public Game(MapTemplate mapTemplate)
        {
            ArgumentNullException.ThrowIfNull(mapTemplate, nameof(mapTemplate));
            Sections = mapTemplate.Sections.Select(x => new Section(this, x)).ToArray();
        }

        internal Random Random { get; } = new Random();

        public Date Time { get; private set; } = new Date(1200 * 360 * 24);

        public bool IsPaused => Speed == GameSpeed.Pause;

        public bool IsRunning { get; private set; } = true;

        public void Stop()
        {
            IsRunning = false;
        }

        public GameSpeed Speed { get; set; } = GameSpeed.Normal;

        public IEnumerable<Section> Sections { get; private set; }

        public IEnumerable<Exception> ExceptionQueue { get; private set; } = [];

        internal void AddException(Exception ex)
        {
            ExceptionQueue = ExceptionQueue.Append(ex);
            OnException.Invoke(this, ex);
        }

        public event EventHandler<Exception> OnException = default!;
        public event EventHandler OnTick = default!;

        public async Task Start()
        {
            IsRunning = true;
            while (IsRunning)
            {
                while (IsPaused) { await Task.Delay(100); }

                Time++;
                foreach (var section in Sections)
                {
                    section.Process();
                }

                OnTick.Invoke(this, EventArgs.Empty);
                await Task.Delay((int)Speed);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.Sections = null!;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Game()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public enum GameSpeed
    {
        Pause = 0,
        Normal = 1000,
        Fast = 500,
        Faster = 250,
    }
}
