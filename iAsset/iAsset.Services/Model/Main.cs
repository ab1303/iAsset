using Newtonsoft.Json;

namespace iAsset.Services.Model
{
    public class Main
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }

        [JsonProperty("temp_min")]
        public double MinimumTemperature { get; set; }

        [JsonProperty("temp_max")]
        public double MaximumTemperature { get; set; }
    }
}
