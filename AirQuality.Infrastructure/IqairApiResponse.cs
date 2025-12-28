namespace AirQuality.Infrastructure
{
    public class IqairApiResponse
    {
        public IqairData data { get; set; }

    }
    public class IqairData
    {
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IqairCurrent current { get; set; }
    }

    public class IqairCurrent
    {
        public IqairPollution pollution { get; set; }
    }

    public class IqairPollution
    {
        public int aqius { get; set; }
        public string mainus { get; set; }
        public DateTime ts { get; set; }

    }
}