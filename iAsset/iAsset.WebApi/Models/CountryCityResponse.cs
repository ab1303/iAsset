using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iAsset.WebApi.Models;

namespace iAsset.WebApi.Models
{
    /// <summary>
    /// A Country Response Object
    /// </summary>
    public class CountryCityResponse : BaseApiResponse
    {
        /// <summary>
        /// List of cities in a country
        /// </summary>
        public IEnumerable<string> Cities { get; set; }
    }
}