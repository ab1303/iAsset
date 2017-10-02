using System;
using System.Collections;
using System.Collections.Generic;
using IAsset.Services.DTO;

namespace IAsset.WebApi.Models.Cheque
{

    public class ChequeResponse : BaseApiResponse
    {
        public WeatherDto Cheque { get; set; }

    }

    public class ChequeListResponse : BaseApiResponse
    {
        public IEnumerable<WeatherDto> Cheques { get; set; }

    }
}