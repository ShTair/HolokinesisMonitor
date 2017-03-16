using System;
using System.Collections.ObjectModel;

namespace HolokinesisMonitor
{
    class MainViewModel : IDisposable
    {
        public ObservableCollection<TargetModel> Targets { get; }

        public MainViewModel()
        {
            Targets = new ObservableCollection<TargetModel>();
            for (int i = 0; i < 10; i++)
            {
                var tm = new TargetModel
                {
                    Id = i + 10,
                    Angle = (360 / 10) * i
                };
                Targets.Add(tm);
                tm.Start();
            }
        }

        public void Dispose()
        {
            foreach (var target in Targets)
            {
                target.Dispose();
            }
        }
    }
}
