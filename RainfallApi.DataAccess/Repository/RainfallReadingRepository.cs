using Newtonsoft.Json;
using RainfallApi.DataAccess.Interface;
using RainfallApi.Domain.Model.Entity;
using System.Net.Http;

namespace RainfallApi.DataAccess.Repository
{
    public class RainfallReadingRepository : IRainfallReadingRepository
    {
        private const string API_URL = "https://environment.data.gov.uk/flood-monitoring/id/stations/{0}/measures?_limit={1}";
        private readonly HttpClient _httpClient;

        public RainfallReadingRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<RainfallData>?> GetReadingByStation(string stationId, int limit)
        {
            var response = await _httpClient.GetAsync(string.Format(API_URL, stationId, limit));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve stations. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var stationsResponse = JsonConvert.DeserializeObject<ExternalRainfallApiResponse>(content);

            return stationsResponse?.Items;
        }
    }
}
