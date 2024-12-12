using HotelFlow.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelFlow.Controllers
{
    public class AddroomController : Controller
    {
        private readonly ApplicationDBContext _db;

        public AddroomController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var roomTable = _db.Addroom
                .FromSqlInterpolated($@"
            SELECT 
                bookings.booking_id     as BookingId,
                bookings.room_id        as RoomId,
                bookings.check_in       as CheckIn,
                bookings.check_out      as CheckOut,
                bookings.booking_status as BookingStatus,
                bookings.sname          as CustomerName,
                rooms.room_type         as RoomType,
                rooms.is_available      as IsAvailable,
                rooms.capacity          as Capacity,
                rooms.price             as Price,
                users.username			as username
            FROM bookings
            LEFT JOIN rooms ON rooms.room_id = bookings.room_id
            LEFT JOIN users ON users.user_id = bookings.user_id
            ORDER BY bookings.booking_id DESC
        ")
                .ToList();

            // คำนวณผลรวมสุดท้ายของราคา
            var full = roomTable.Count(r => r.IsAvailable == 0);     // เต็ม
            var empty = roomTable.Count(r => r.IsAvailable == 1);    // ว่าง
            var reserve = roomTable.Count(r => r.IsAvailable == 2);  // จอง

            // ส่งข้อมูลไปยัง View ผ่าน ViewData (optional if you need)
            ViewData["Full"] = full;
            ViewData["Empty"] = empty;
            ViewData["Reserve"] = reserve;

            // ส่งข้อมูลของ query ไปยัง View (Model)
            return View(roomTable);
        }

    }
}
