using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
    [RoutePrefix("api/v1")]
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

        /// <summary>
        /// Get list of cities by country name
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        //[HttpGet]
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
                countryResposne.Message = "list of cities retrieved";


                return Request.CreateResponse(HttpStatusCode.OK, countryResposne);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        /// <summary>
        /// Get the weather condition of a particular city of a country
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("{country}/{city}/weather")]
        [ResponseType(typeof(WeatherResponse))]
        public HttpResponseMessage Get([FromUri] WeatherRequest request)
        {

            if (string.IsNullOrEmpty(request.Country) || string.IsNullOrWhiteSpace(request.Country))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "country is invalid"
                });
            }

            if (string.IsNullOrEmpty(request.City) || string.IsNullOrWhiteSpace(request.City))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "city is invalid"
                });
            }

            var countryCityResposne = new CountryCityResponse();
            var countryCityResult = _weatherService.GetCities(request.Country);

            if (!countryCityResult.IsSuccess)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Error,
                    Message = "Failed to fetch cities"
                });

            if (!countryCityResult.Cities.Any(c => c.Equals(request.City)))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Error,
                    Message = "City doesn't below to this country"
                });


            var weatherResposne = new WeatherResponse();
            try
            {
                var result = _weatherService.GetCityWeather(request.City);

                if (!result.IsSuccess)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "Failed to fetch cities"
                    });

                weatherResposne.Weather = result.CityWeather;
                weatherResposne.Code = InternalApiStatusCode.Success;
                weatherResposne.Message = "City weather condition";


                return Request.CreateResponse(HttpStatusCode.OK, weatherResposne);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
