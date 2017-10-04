using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using iAsset.Services.Interfaces;
using iAsset.WebApi.Models.iAsset;
using iAsset.WebApi.Models.iAsset.Weather;

namespace iAsset.WebApi.Controllers
{
    /// <summary>
    /// Weather Controller
    /// </summary>
    [RoutePrefix("v1/Country")]
    public class WeatherController : ApiController
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Weather controller contructor
        /// </summary>
        /// <param name="weatherService"></param>
        /// <param name="mapper"></param>
        public WeatherController(IWeatherService weatherService, IMapper mapper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
        }


        // GET: api/country/Australia

        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("{country}/cities")]
        [ResponseType(typeof(CountryCityResponse))]
        public HttpResponseMessage Get(string country)
        {

            if (string.IsNullOrEmpty(country) || string.IsNullOrWhiteSpace(country))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "country is invalid"
                });
            }


            var countryResposne = new CountryCityResponse();
            try
            {
                var result = _weatherService.GetCities(country);

                if (!result.IsSuccess)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "Failed to fetch cities"
                    });

                countryResposne.Cities = result.Cities;
                countryResposne.Code = InternalApiStatusCode.Success;
                countryResposne.Message = "cheque item added";


                return Request.CreateResponse(HttpStatusCode.OK, countryResposne);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("{country}/{city}/weather")]
        [ResponseType(typeof(WeatherResponse))]
        public HttpResponseMessage Get([FromBody] WeatherRequest request)
        {

            if (string.IsNullOrEmpty(request.CountryName) || string.IsNullOrWhiteSpace(request.CountryName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "country is invalid"
                });
            }

            if (string.IsNullOrEmpty(request.CityName) || string.IsNullOrWhiteSpace(request.CityName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "city is invalid"
                });
            }


            var weatherResposne = new WeatherResponse();
            try
            {
                var result = _weatherService.GetCityWeather(request.CityName);

                if (!result.IsSuccess)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "Failed to fetch cities"
                    });

                weatherResposne.Weather = result.CityWeather;
                weatherResposne.Code = InternalApiStatusCode.Success;
                weatherResposne.Message = "cheque item added";


                return Request.CreateResponse(HttpStatusCode.OK, weatherResposne);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
