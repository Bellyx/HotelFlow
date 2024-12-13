using HotelFlow.Data;
using HotelFlow.Models;
using HotelFlow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelFlow.Controllers
{
    public class AddroomController : Controller
    {
        private readonly ApiService _apiService;
        public AddroomController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<ActionResult> Index()
        {
            // เรียกข้อมูลจาก API
            var addrooms = await _apiService.GetAddroomsAsync();

            // ส่งข้อมูลไปแสดงใน View
            return View(addrooms);
        }
        // เพิ่มห้องใหม่
        public async Task<ActionResult> Create(Addroom addroom)
        {
            var createdAddroom = await _apiService.CreateAddroomAsync(addroom);

            return View(createdAddroom);
            //return RedirectToAction(createdAddroom);
        }

        // แก้ไขห้องที่มีอยู่
        public async Task<ActionResult> Edit(int id, Addroom addroom)
        {
            var success = await _apiService.UpdateAddroomAsync(id, addroom);
            if (success)
                return RedirectToAction("Index");
            return View();
        }

        // ลบห้อง
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _apiService.DeleteAddroomAsync(id);
            if (success)
                return RedirectToAction("Index");
            return View();
        }
        // Action ที่รับข้อมูลจากฟอร์ม
        //[HttpPost]
        //public async Task<IActionResult> AddroomAsync(string BOOKING_ID, string roomDescription,int RoomId)
        //{
        //    string newBookingId = await GenerateNewBookingId(); // คุณสามารถใช้วิธีการสร้างรหัสที่คุณต้องการ

        //    var newRoom = new Dashboard
        //    {
        //        BookingId = newBookingId,
        //        RoomId = RoomId,
        //    }; 

        //    //            new SqlParameter("@BOOKING_ID", newBookingId),
        //    //            new SqlParameter("@ROOM_ID", roomBooking.RoomId),
        //    //          //  new SqlParameter("@USER_ID", roomBooking.UserId),
        //    //            new SqlParameter("@CHECK_IN", roomBooking.CheckIn),
        //    //            new SqlParameter("@CHECK_OUT", roomBooking.CheckOut),
        //    //            new SqlParameter("@TOTAL_PRICE", roomBooking.Price),
        //    //            new SqlParameter("@SNAME", roomBooking.CustomerName)



        //    _db.Dashboard.Add(newRoom); // _context คือ DbContext ของคุณ
        //    _db.SaveChanges(); // บันทึกข้อมูลลงฐานข้อมูล

        //    return RedirectToAction("Index");
        //}

        //private async Task<string> GenerateNewBookingId()
        //{
        //    //string newBookingId = string.Empty;

        //    //var lastBooking = await _db.Dashboard
        //    //.FromSqlInterpolated($"SELECT TOP 1 BookingId FROM Dashboard ORDER BY BookingId DESC")
        //    //.FirstOrDefaultAsync();
        //    //if (lastBooking != null)
        //    //{
        //    //    // แปลงรหัสที่ได้ (เช่น BKG003) และเพิ่มเลขให้กับรหัสใหม่
        //    //    string lastBookingId = lastBooking.ToString();
        //    //    int lastNumber = int.Parse(lastBookingId.Substring(3)); // ตัด 'BKG' ออกและแปลงเป็นตัวเลข
        //    //    int newNumber = lastNumber + 1; // เพิ่ม 1 เพื่อให้ได้รหัสใหม่

        //    //    newBookingId = "BKG" + newNumber.ToString("D3"); // สร้างรหัสใหม่ เช่น BKG004
        //    //}
        //    //else
        //    //{
        //    //    // ถ้ายังไม่มีข้อมูลในตาราง ให้เริ่มจาก BKG001
        //    //    newBookingId = "BKG001";
        //    //}
        //    //return newBookingId;
        //}
    }
}
