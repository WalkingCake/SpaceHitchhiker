using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Massive;


namespace SpaceHitchhiker.Planets
{
    public class PlanetRawInfo : AbstractRawInfo<Planet>
    {
        public string Name { get; }
        public float DistanceFromSolar { get; }
        public float DistanceToHitchhiker { get; }
        public float Angle { get; }
        public float BornDeltaTime { get; }
        public float SpinDeltaTime { get; }
        public float PositionSwitchingTime { get; }
        public int Radius { get; }
        public string EventID { get; }
        public LimitedMassiveRawInfo MassiveInfo { get; }

        public PlanetRawInfo(string name, float distanceFromSolar, float distanceToHitchhiker,
            float angle, int radius, float spinDeltaTime, float bornDeltaTime,
            float positionSwitchingTime, string eventID, LimitedMassiveRawInfo massiveInfo)
        {
            this.Name = name;
            this.DistanceFromSolar = distanceFromSolar;
            this.DistanceToHitchhiker = distanceToHitchhiker;
            this.Angle = angle;
            this.BornDeltaTime = bornDeltaTime;
            this.SpinDeltaTime = spinDeltaTime;
            this.PositionSwitchingTime = positionSwitchingTime;
            this.Radius = radius;
            this.EventID = eventID;
            this.MassiveInfo = massiveInfo;
        }

    }
}
