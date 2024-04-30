using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BNSegurosMAUI.WebServices.Client
{
    public class WebService
    {
        private readonly string _servicePath;
        private readonly HttpMethod _httpMethod;
        private readonly IRequestBody _requestBody;

        public WebService(string servicePath, HttpMethod httpMethod, IRequestBody requestBody = null)
        {
            _servicePath = servicePath;
            _httpMethod = httpMethod;
            _requestBody = requestBody;
        }

        public async Task<T> Execute<T>() where T : IResponse, new()
        {
            var clientHandler = new HttpClientHandler();
            var request = new HttpRequestMessage(_httpMethod, _servicePath);

            if (_requestBody != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(_requestBody, GetSnakeCaseSerializerSettings()), Encoding.UTF8, "application/json");
            }

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ApiConstants.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(ApiConstants.DefaultTimeout);

                T response;
                try
                {
                    var httpResponse = await client.SendAsync(request);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        response = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync(), GetSnakeCaseSerializerSettings());
                        if (response == null)
                        {
                            response = GetDefaultErrorResponse<T>(200);
                        }
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync(), GetSnakeCaseSerializerSettings());
                        if (response == null || response.Errors == null)
                        {
                            response = GetDefaultErrorResponse<T>((int)httpResponse.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    response = GetDefaultErrorResponse<T>();
                }
                return response;
            }
        }

        private T GetDefaultErrorResponse<T>(int statusCode = -1) where T : IResponse, new()
        {
            var errorResponse = new T();
            errorResponse.SetDefaultErrorState(statusCode);
            return errorResponse;
        }

        private JsonSerializerSettings GetSnakeCaseSerializerSettings()
        {
            var contractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            var serializerSettings = new JsonSerializerSettings { ContractResolver = contractResolver };
            return serializerSettings;
        }
    }
}
