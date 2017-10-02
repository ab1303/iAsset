using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using IAsset.Services.DTO;
using IAsset.Services.Interfaces;
using IAsset.WebApi.Models;
using IAsset.WebApi.Models.Cheque;

namespace IAsset.WebApi.Controllers
{
    public class ChequesController : ApiController
    {
        private readonly IWeatherRepository _chequeRepository;
        private readonly IMapper _mapper;

        public ChequesController(IWeatherRepository chequeRepository, IMapper mapper)
        {
            _chequeRepository = chequeRepository;
            _mapper = mapper;
        }


        [AcceptVerbs("GET")]
        [ResponseType(typeof(IEnumerable<ChequeListResponse>))]
        public HttpResponseMessage Get()
        {

            var chequeListResponse = new ChequeListResponse();
            try
            {

                chequeListResponse.Cheques = _chequeRepository.Get();
                chequeListResponse.Code = InternalApiStatusCode.Success;

                return Request.CreateResponse(HttpStatusCode.OK, chequeListResponse);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // GET: api/cheques/5

        [HttpGet]
        [AcceptVerbs("GET")]
        [ResponseType(typeof(ChequeResponse))]
        public HttpResponseMessage Get(int id)
        {

            if (id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "id is invalid"
                });
            }


            var chequeResposne = new ChequeResponse();
            try
            {
                var cheque = _chequeRepository.Get(id);

                if (cheque == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "cheque item is not found"
                    });

                chequeResposne.Cheque = cheque;
                chequeResposne.Code = InternalApiStatusCode.Success;
                chequeResposne.Message = "cheque item added";


                return Request.CreateResponse(HttpStatusCode.OK, chequeResposne);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // POST: api/cheques

        [AcceptVerbs("POST")]
        [ResponseType(typeof(ChequeResponse))]
        public HttpResponseMessage Post([FromBody]WeatherDto cheque)
        {
            if (cheque == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Error,
                    Message = "Invalid cheque object"
                });

            }

            if (string.IsNullOrEmpty(cheque.LastName) || string.IsNullOrEmpty(cheque.FirstName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "Invalid cheque object, name is not provided"
                });

            }

            if (cheque.Amount <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "Invalid cheque object, amount should be greater than zero"
                });

            }


            var chequeResposne = new ChequeResponse();
            try
            {
                var chequeResult = _chequeRepository.Add(cheque);

                chequeResposne.Cheque = chequeResult;
                chequeResposne.Code = InternalApiStatusCode.Success;

                return Request.CreateResponse(HttpStatusCode.Created, chequeResposne);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // PUT: api/cheques



        [ResponseType(typeof(ChequeResponse))]
        public HttpResponseMessage Put(int id, [FromBody]ChequeRequest cheque)
        {

            if (string.IsNullOrEmpty(cheque.LastName) || string.IsNullOrEmpty(cheque.FirstName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.FailedRequestValidation,
                    Message = "Invalid cheque object"
                });

            }

            var chequeResposne = new ChequeResponse();
            try
            {
                var chequeResult = _chequeRepository.Update(new WeatherDto
                {
                    ChequeId = id,
                    LastName = cheque.LastName,
                    FirstName = cheque.FirstName,
                    Amount = cheque.Amount,
                });

                if (chequeResult == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new BaseApiResponse
                    {
                        Code = InternalApiStatusCode.Error,
                        Message = "cheque is not found"
                    });

                chequeResposne.Cheque = chequeResult;
                chequeResposne.Code = InternalApiStatusCode.Success;
                chequeResposne.Message = "cheque is updated";

                return Request.CreateResponse(HttpStatusCode.OK, chequeResposne);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // DELETE: api/cheques/5
        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(int id)
        {
            var cheque = _chequeRepository.Get(id);

            if (cheque == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Error,
                    Message = "cheque item is not found"
                });

            try
            {
                _chequeRepository.Remove(id);

                return Request.CreateResponse(HttpStatusCode.OK, new BaseApiResponse
                {
                    Code = InternalApiStatusCode.Success,
                    Message = "cheque item removed"
                });

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        //[HttpOptions]
        //public HttpResponseMessage Options()
        //{
        //    var response = new HttpResponseMessage();
        //    response.StatusCode = HttpStatusCode.OK;
        //    return response;
        //}

    }
}
