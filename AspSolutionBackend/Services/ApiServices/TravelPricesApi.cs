using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Newtonsoft.Json.Converters;
using Utils;

namespace Services.ApiServices
{
    public class TravelPricesApi
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


            return DeserializeStreamAsync<TravelPrices>(responseJson);
        }

        private static T DeserializeStreamAsync<T>(string responseJson)
        {
            JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };
            jsonOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());

            return JsonSerializer.Deserialize<T>(responseJson, jsonOptions)!;
        }
    }
}