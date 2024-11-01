﻿using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.WebRequestMethods;

namespace MagicVilla_Web.Services
{

    public class VillaNumberService : BaseServices, IVillaNumberService
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private string ApiUrl;
        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _configuration = configuration;
            _clientFactory = httpClientFactory;
            _configuration.GetValue<string>("ServiceUrls:VillaApi");

            ApiUrl = _configuration.GetValue<string>("ServiceUrls:VillaApi") + "/api/VillaNumber/";

        }

        public Task<T> CreateAsync<T>(CreateVillaNumberRequest dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                URL = ApiUrl

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                Data = id,
                URL = ApiUrl + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {

            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,

                URL = ApiUrl
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                URL = ApiUrl + id
            });
        }

        public Task<T> UpdateAsync<T>(UpdateVillaNumber dto, int VillaNO)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.PUT,
                Data = dto,
                URL = ApiUrl + VillaNO
            });
        }
    }
}
