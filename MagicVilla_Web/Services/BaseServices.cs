using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace MagicVilla_Web.Services
{
    public class BaseServices : IBaseService
    {
        private IHttpClientFactory _httpClientFactory;
        public ApiResponse responseModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BaseServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Client");

                HttpRequestMessage message = new();

                message.RequestUri = new Uri(apiRequest.URL);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");

                    switch (apiRequest.ApiType)
                    {
                        case ApiType.POST:
                            message.Method = HttpMethod.Post;
                            break;

                        case ApiType.PUT:
                            message.Method = HttpMethod.Put;
                            break;

                        case ApiType.DELETE:
                            message.Method = HttpMethod.Delete;
                            break;

                        default:
                            message.Method = HttpMethod.Get;

                            break;
                    }
                }


                HttpResponseMessage response = await client.SendAsync(message);

                var apiContent = await response.Content.ReadAsStringAsync();

                try
                {
                    ApiResponse? apiResponse = JsonConvert.DeserializeObject<ApiResponse>(apiContent);

                    if (apiResponse?.StatusCode == HttpStatusCode.BadRequest ||
                            apiResponse?.StatusCode == HttpStatusCode.NotFound)
                    {
                        apiResponse.StatusCode = response.StatusCode;
                        apiResponse.IsSuccess = false;

                        var res = JsonConvert.SerializeObject(apiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);

                        return returnObj;
                    }

                }
                catch
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var API_Response = JsonConvert.DeserializeObject<T>(apiContent);

                return API_Response;

            }
            catch (Exception e)
            {

                var dto = new ApiResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}