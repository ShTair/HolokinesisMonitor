namespace HolokinesisMonitor
{
    class TargetModel
    {
        public int Id { get; set; }

        public double Angle { get; set; }

        public double Angle2 => -Angle;
    }
}
