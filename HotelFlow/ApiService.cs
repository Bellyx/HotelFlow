using HotelFlow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//namespace HotelFlow
//{
//    public class ApiService
//    {
//        private readonly HttpClient _httpClient;
//        public ApiService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        // ดึงข้อมูลรายการ Addroom ทั้งหมด
//        public async Task<List<Addroom>> GetAddroomsAsync()
//        {
//            var response = await _httpClient.GetAsync("/api/Addroom");
//            response.EnsureSuccessStatusCode();

//            var addrooms = await response.Content.ReadFromJsonAsync<List<Addroom>>();
//            return addrooms;
//        }

//        // ดึงข้อมูล Addroom รายการเดียว
//        public async Task<Addroom> GetAddroomByIdAsync(int id)
//        {
//            var response = await _httpClient.GetAsync($"/api/Addroom/{id}");
//            response.EnsureSuccessStatusCode();

//            var addroom = await response.Content.ReadFromJsonAsync<Addroom>();
//            return addroom;
//        }

//        // ส่งข้อมูลเพื่อสร้าง Addroom
//        public async Task<Addroom> CreateAddroomAsync(Addroom addroom)
//        {
//            var response = await _httpClient.PostAsJsonAsync("/api/Addroom", addroom);
//            response.EnsureSuccessStatusCode();

//            var createdAddroom = await response.Content.ReadFromJsonAsync<Addroom>();
//            return createdAddroom;
//        }

//        // ส่งข้อมูลเพื่ออัปเดต Addroom
//        public async Task<bool> UpdateAddroomAsync(int id, Addroom addroom)
//        {
//            var response = await _httpClient.PutAsJsonAsync($"/api/Addroom/{id}", addroom);
//            return response.IsSuccessStatusCode;
//        }

//        // ส่งคำขอเพื่อลบ Addroom
//        public async Task<bool> DeleteAddroomAsync(int id)
//        {
//            var response = await _httpClient.DeleteAsync($"/api/Addroom/{id}");
//            return response.IsSuccessStatusCode;
//        }
//    }

//}
