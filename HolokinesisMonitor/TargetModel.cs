using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace HolokinesisMonitor
{
    class TargetModel : INotifyPropertyChanged, IDisposable
    {
        public int Id { get; set; }

        public double Angle { get; set; }

        public double Angle2 => -Angle;

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<bool> StatusChanged;

        public bool Status
        {
            get { return _Status; }
            private set
            {
                if (_Status == value) return;
                _Status = value;
                Color = value ? "Lime" : "Red";
                StatusChanged?.Invoke(value);
                PropertyChanged?.Invoke(this, _StatusChangedEventArgs);
            }
        }
        private bool _Status;
        private PropertyChangedEventArgs _StatusChangedEventArgs = new PropertyChangedEventArgs(nameof(Status));

        public string Color
        {
            get { return _Color; }
            private set
            {
                if (_Color == value) return;
                _Color = value;
                PropertyChanged?.Invoke(this, _ColorChangedEventArgs);
            }
        }
        private string _Color = "Red";
        private PropertyChangedEventArgs _ColorChangedEventArgs = new PropertyChangedEventArgs(nameof(Color));

        public bool IsDisposed { get; private set; }

        public async void Start()
        {
            while (!IsDisposed)
            {
                using (var ping = new Ping())
                {
                    var res = await ping.SendPingAsync("192.168.100." + Id);
                    Status = res.Status == IPStatus.Success;
                }
                await Task.Delay(2000);
            }
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
        }
    }
}
