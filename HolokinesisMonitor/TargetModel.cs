using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace HolokinesisMonitor
{
    class TargetModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public double Angle { get; set; }

        public double Angle2 => -Angle;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Color
        {
            get { return _Color; }
            set
            {
                if (_Color == value) return;
                _Color = value;
                PropertyChanged?.Invoke(this, _ColorChangedEventArgs);
            }
        }
        private string _Color;
        private PropertyChangedEventArgs _ColorChangedEventArgs = new PropertyChangedEventArgs(nameof(Color));

        public TargetModel()
        {
            Color = "Red";
        }

        public async void Start()
        {
            using (var ping = new Ping())
            {
                while (true)
                {
                    var res = await ping.SendPingAsync("192.168.10." + Id);
                    if (res.Status == IPStatus.Success)
                    {
                        Color = "Lime";
                    }
                    else
                    {
                        Color = "Red";
                    }

                    await Task.Delay(2000);
                }
            }
        }
    }
}
