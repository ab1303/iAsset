using System;
using System.Collections;
using System.Collections.Generic;
using iAsset.Services.DTO;

namespace iAsset.WebApi.Models.Weather
{

    public class WeatherResponse : BaseApiResponse
    {
        public WeatherDto Weather { get; set; }

    }

   }