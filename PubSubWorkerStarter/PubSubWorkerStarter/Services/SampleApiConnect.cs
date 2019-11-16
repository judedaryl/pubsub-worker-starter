using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using PubSubWorkerStarter.Contracts;

namespace PubSubWorkerStarter.Services
{
    internal class SampleApiConnect : IApiConnect<string>
    {
        private readonly HttpClient _httpClient;
        public SampleApiConnect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetSampleData(long id)
        {
            var httpResponseString = await _httpClient.GetStringAsync($"api/v1/sample/{id}");
            var response = JsonSerializer.Deserialize<string>(httpResponseString);

            return response;
        }

    }
}
