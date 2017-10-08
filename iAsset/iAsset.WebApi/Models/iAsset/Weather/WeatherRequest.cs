namespace iAsset.WebApi.Models.iAsset.Weather
{
    /// <summary>
    /// Weather Request
    /// </summary>
    public class WeatherRequest
    {
        /// <summary>
        /// Country name parameter
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// City name parameter
        /// </summary>
        public string City { get; set; }

    }
}