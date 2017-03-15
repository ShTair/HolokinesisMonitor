using System.Collections.ObjectModel;

namespace HolokinesisMonitor
{
    class MainViewModel
    {
        public ObservableCollection<TargetModel> Targets { get; }

        public MainViewModel()
        {
            Targets = new ObservableCollection<TargetModel>();
            for (int i = 0; i < 10; i++)
            {
                Targets.Add(new TargetModel
                {
                    Id = i + 10,
                    Angle = (360 / 10) * i
                });
            }
        }
    }
}
