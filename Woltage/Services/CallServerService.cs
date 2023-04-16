using Newtonsoft.Json;
using System.Text;

namespace Woltage.Services
{
    public class CallServerService
    {
        private readonly HttpClient _httpClient;

        public CallServerService()
        {
            _httpClient = new HttpClient();
        }

        public T Get<T>(string apiUrl)
        {
            var response = _httpClient.GetAsync(apiUrl);
            var responseBody = response.Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody.Result);
        }

        public T Post<T, TZ>(TZ request, string apiUrl)
        {
            var response = _httpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            var responseBody = response.Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody.Result);
        }
    }
}
