using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Utils;

namespace Services.ApiServices
{
    public static class TravelPricesApi
    {
        private const string BaseUrl = "https://cosmos-odyssey.azurewebsites.net/api/v1.0/";
        private const string TravelUrl = BaseUrl + "/TravelPrices/";
        private static HttpClient _client;


        static TravelPricesApi()
        {
            _client = new HttpClient();
        }

        public static async Task<TravelPrices> GetCurrentTravelPrices()
        {
            var response = await _client.GetAsync(TravelUrl);

            var responseJson = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            var res = DeserializeWithIsoDates<TravelPrices>(responseJson);
            res.ValidUntil = DateConvertors.ConvertDateTimeToEstonian(res.ValidUntil);
            return res;
        }

        private static T DeserializeWithIsoDates<T>(string responseJson)
        {
            JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };
            jsonOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());

            return JsonSerializer.Deserialize<T>(responseJson, jsonOptions)!;
        }
    }
}