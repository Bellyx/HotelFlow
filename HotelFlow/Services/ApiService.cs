using HotelFlow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFlow.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// สำหรับ หน้า AddRoom เพิ่มห้อง
        public async Task<List<Addroom>> GetAddroomsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Addroom");
            response.EnsureSuccessStatusCode();
            var addrooms = await response.Content.ReadFromJsonAsync<List<Addroom>>();
            return addrooms;
        }

        public async Task<Addroom> GetAddroomByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Addroom/{id}");
            response.EnsureSuccessStatusCode();

            var addroom = await response.Content.ReadFromJsonAsync<Addroom>();
            return addroom;
        }

        public async Task<Addroom> CreateAddroomAsync(Addroom addroom)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Addroom", addroom);
            response.EnsureSuccessStatusCode();

            var createdAddroom = await response.Content.ReadFromJsonAsync<Addroom>();
            return createdAddroom;
        }

        public async Task<bool> UpdateAddroomAsync(int id, Addroom addroom)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Addroom/{id}", addroom);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAddroomAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Addroom/{id}");
            return response.IsSuccessStatusCode;
        }

        /// สิ้นสุด สำหรับ หน้า AddRoom เพิ่มห้อง

    }
}


