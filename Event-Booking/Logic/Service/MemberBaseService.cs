using Azure;
using Core.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Core.DTO.MemberApiDTO;

namespace Logic.Service
{
    public class MemberBaseService : IMemberBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly MemberBaseSettings _settings;

        public MemberBaseService(HttpClient httpClient, IOptions<MemberBaseSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

            // Set base URL once
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        public async Task<ApiResponse<ContactResponseData>> CreateContactAsync(CreateContactRequest req)
        {
            var endpoint = $"{_settings.BaseUrl}/api/v1/contacts";

            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, endpoint );
                request.Headers.Add("Authorization", "Bearer " + _settings.Token);

                var json = JsonSerializer.Serialize(req);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<ContactResponseData>()
                    {
                        Message = $"API returned error ({response.StatusCode}): {responseBody}"
                    };
                }
                var result = JsonSerializer.Deserialize<ApiResponse<ContactResponseData>>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result;

            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<ContactResponseData>()
                {
                    Message = "Network or connection error occurred while calling MemberBase API."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ContactResponseData>()
                {
                    Message = $"Unexpected error while processing request: {ex.Message}"
                };
            }
        }
    }
}
