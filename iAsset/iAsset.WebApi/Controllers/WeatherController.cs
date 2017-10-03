using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using iAsset.Services.Interfaces;
using iAsset.WebApi.Models;
using iAsset.Services.DTO;

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
                    Message = "id is invalid"
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


        // POST: api/cheques

      
        //[HttpOptions]
        //public HttpResponseMessage Options()
        //{
        //    var response = new HttpResponseMessage();
        //    response.StatusCode = HttpStatusCode.OK;
        //    return response;
        //}

    }
}
