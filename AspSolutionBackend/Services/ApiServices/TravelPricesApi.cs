using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Newtonsoft.Json.Linq;

namespace Services.ApiServices
{
    public class TravelPricesApi
    {
        private const string BaseUrl = "https://cosmos-odyssey.azurewebsites.net/api/v1.0/";
        private const string TravelUrl = BaseUrl + "/TravelPrices/";
        private static HttpClient _client; // maybe use restClient?


        static TravelPricesApi()
        {
            _client = new HttpClient();
        }

        public static async Task<TravelPrices> GetCurrentTravelPrices()
        {
            var response = await _client.GetAsync(TravelUrl);

            var responseJson = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            

            return DeserializeStreamAsync<TravelPrices>(responseJson);
        }

        private static T DeserializeStreamAsync<T>(string responseJson)
        {
            JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true, };
            return JsonSerializer.Deserialize<T>(responseJson, jsonOptions)!;
            
            // return (await JsonSerializer.DeserializeAsync<T>(responseJson, new JsonSerializerOptions(new JsonSerializerOptions())))!;
            // return (await JsonSerializer.DeserializeAsync<T>(responseJson, new JsonSerializerOptions(new JsonSerializerOptions())))!;
        }
    }
}