using System.Collections.Generic;
using Newtonsoft.Json;

namespace iAsset.Services.Model
{
    public class WeatherResponse
    {
        [JsonProperty("coord")]
        public Coordinate Coordinate { get; set; }

        public IEnumerable<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public double Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
        public double Dt { get; set; }
        public Sys Sys { get; set; }
    }
}
