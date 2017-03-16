using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace HolokinesisMonitor
{
    class MainViewModel : IDisposable, INotifyPropertyChanged
    {
        private Dispatcher _d;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TargetModel> Targets { get; }

        public ObservableCollection<string> Logs { get; }

        private int _count;

        public double Percentage
        {
            get { return _Percentage; }
            set
            {
                if (_Percentage == value) return;
                _Percentage = value;
                Angle = value / 100 * 360;
                PropertyChanged?.Invoke(this, _PercentageChangedEventArgs);
            }
        }
        private double _Percentage;
        private PropertyChangedEventArgs _PercentageChangedEventArgs = new PropertyChangedEventArgs(nameof(Percentage));

        public double Angle
        {
            get { return _Angle; }
            set
            {
                if (_Angle == value) return;
                _Angle = value;
                PropertyChanged?.Invoke(this, _AngleChangedEventArgs);
            }
        }
        private double _Angle;
        private PropertyChangedEventArgs _AngleChangedEventArgs = new PropertyChangedEventArgs(nameof(Angle));

        public MainViewModel()
        {
            _d = Dispatcher.CurrentDispatcher;

            Targets = new ObservableCollection<TargetModel>();
            Logs = new ObservableCollection<string>();

            for (int i = 0; i < 10; i++)
            {
                var tm = new TargetModel
                {
                    Id = i + 10,
                    Angle = (360 / 10) * i
                };
                tm.StatusChanged += status =>
                {
                    _d.Invoke(() =>
                    {
                        if (status) _count++;
                        else _count--;
                        Percentage = (double)_count * 100 / 10;

                        Logs.Insert(0, $"{DateTime.Now:HH:mm:ss} {tm.Id} {(status ? "Connected" : "Disconnected")}");
                        while (Logs.Count > 50)
                        {
                            Logs.RemoveAt(Logs.Count - 1);
                        }
                    });
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
