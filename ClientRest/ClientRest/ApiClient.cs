using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientRest
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }


        public async Task<JocCreateDTO> SaveJocAsync(JocCreateDTO joc)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/jocuri", joc);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JocCreateDTO>();
        }
        
    }
}