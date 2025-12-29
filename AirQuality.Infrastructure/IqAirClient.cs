using AirQuality.Application.DTO;
using AirQuality.Application.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Infrastructure
{
        public class IqAirClient : IQAir
        {
            private readonly RestClient _client;
            private readonly string _apiKey;

            public IqAirClient(string apiKey)
            {
                _apiKey = apiKey;
                _client = new RestClient("https://api.airvisual.com/v2/");
            }

            public async Task<AirQualityResponseDto> GetAirQualityAsync(double latitude, double longitude)
            {
                try
                {
                    var request = new RestRequest("nearest_city", Method.Get);
                    request.AddQueryParameter("lat", latitude.ToString());
                    request.AddQueryParameter("lon", longitude.ToString());
                    request.AddQueryParameter("key", _apiKey);

                    var response = await _client.ExecuteAsync(request);

                    if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                        throw new ApplicationException("Failed to fetch air quality from IQAir API.");

                    // Deserialize JSON to DTO
                    var apiData = JsonConvert.DeserializeObject<IqairApiResponse>(response.Content);

                    if (apiData == null || apiData.data == null)
                        throw new ApplicationException("Invalid data received from IQAir API.");

                    var pollution = apiData.data.current.pollution;

                    return new AirQualityResponseDto
                    {
                        City = apiData.data.city,
                        State = apiData.data.state,
                        Country = apiData.data.country,
                        AqiUS = pollution.aqius,
                        MainPollutant = pollution.mainus,
                        Timestamp = pollution.ts,
                        Rawjson = response.Content
                    };
                }
                catch (TaskCanceledException)
                {
                    throw new TimeoutException("IQAir API request timed out.");
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error calling IQAir API.", ex);
                }
            }
        }
    }

