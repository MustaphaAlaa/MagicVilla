using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using static System.Net.WebRequestMethods;

namespace MagicVilla_Web.Services
{

    public class VillaService : BaseServices, IVillaService
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private string ApiUrl;
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _configuration = configuration;
            _clientFactory = httpClientFactory;
            _configuration.GetValue<string>("ServiceUrls:VillaApi");

            ApiUrl = _configuration.GetValue<string>("ServiceUrls:VillaApi");

        }

        public Task<T> CreateAsync<T>(CreateVillaRequest dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                URL = ApiUrl + "/api/VillaAPI"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAllAsync<T>()
        {

            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,

                URL = ApiUrl + "/api/villaapi"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(VillaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
