namespace HotelFlow.Controllers
{
    using HotelFlow.Data;
    using HotelFlow.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : Controller
    {

        private readonly ApplicationDBContext _db;

        public DashboardController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            // ดึงข้อมูลจากฐานข้อมูลเกี่ยวกับห้องพัก
            var roomTable = _db.Dashboard
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
            var totalSum = roomTable.Sum(r => r.Price);
            var full = roomTable.Count(r => r.IsAvailable == 0);     // เต็ม
            var empty = roomTable.Count(r => r.IsAvailable == 1);    // ว่าง
            var reserve = roomTable.Count(r => r.IsAvailable == 2);  // จอง

            // ส่งข้อมูลไปยัง View
            ViewBag.full = full;
            ViewBag.empty = empty;
            ViewBag.reserve = reserve;
            ViewBag.TotalSum = totalSum;

            // ส่งข้อมูลของ query ที่สองไปยัง View
            ViewBag.BookingId = roomTable;
            ViewBag.Name = roomTable;
            ViewBag.RoomType = roomTable;
            ViewBag.BookingStatus = roomTable;
            ViewBag.CheckIn = roomTable;
            ViewBag.CheckOut = roomTable;
            ViewBag.Username = roomTable;

            return View(roomTable);
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Dashboard/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //public async Task<IActionResult> Create(Dashboard roomBooking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // สร้างรหัสการจองใหม่
        //        string newBookingId = await GenerateNewBookingId(); // คุณสามารถใช้วิธีการสร้างรหัสที่คุณต้องการ

        //        // คำสั่ง SQL สำหรับการเพิ่มข้อมูล
        //        string query = @"
        //    INSERT INTO BOOKING 
        //    (BOOKING_ID, ROOM_ID, USER_ID, CHECK_IN, CHECK_OUT, TOTAL_PRICE, BOOKING_STATUS, CREATE_AT, UPDATE_AT, SNAME)
        //    VALUES (@BOOKING_ID, @ROOM_ID, @USER_ID, @CHECK_IN, @CHECK_OUT, @TOTAL_PRICE, 'Confirmed', GETDATE(), GETDATE(), @SNAME)";

        //        // ใช้ ExecuteSqlInterpolatedAsync เพื่อส่งคำสั่ง SQL แบบ Interpolated
        //        var parameters = new[]
        //        {
        //            new SqlParameter("@BOOKING_ID", newBookingId),
        //            new SqlParameter("@ROOM_ID", roomBooking.RoomId),
        //          //  new SqlParameter("@USER_ID", roomBooking.UserId),
        //            new SqlParameter("@CHECK_IN", roomBooking.CheckIn),
        //            new SqlParameter("@CHECK_OUT", roomBooking.CheckOut),
        //            new SqlParameter("@TOTAL_PRICE", roomBooking.Price),
        //            new SqlParameter("@SNAME", roomBooking.CustomerName)
        //        };
        //        // Execute SQL command with parameters
        //        await _db.Database.ExecuteSqlRawAsync(query, parameters);

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(roomBooking);
        //}


        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //private async Task<string> GenerateNewBookingId()
        //{
        //    string newBookingId = string.Empty;

        //    var lastBooking = await _db.Dashboard
        //    .FromSqlInterpolated($"SELECT TOP 1 BookingId FROM Dashboard ORDER BY BookingId DESC")
        //    .FirstOrDefaultAsync();
        //    if (lastBooking != null)
        //    {
        //        // แปลงรหัสที่ได้ (เช่น BKG003) และเพิ่มเลขให้กับรหัสใหม่
        //        string lastBookingId = lastBooking.ToString();
        //        int lastNumber = int.Parse(lastBookingId.Substring(3)); // ตัด 'BKG' ออกและแปลงเป็นตัวเลข
        //        int newNumber = lastNumber + 1; // เพิ่ม 1 เพื่อให้ได้รหัสใหม่
                
        //        newBookingId = "BKG" + newNumber.ToString("D3"); // สร้างรหัสใหม่ เช่น BKG004
        //    }
        //    else
        //    {
        //        // ถ้ายังไม่มีข้อมูลในตาราง ให้เริ่มจาก BKG001
        //        newBookingId = "BKG001";
        //    }
        //    return newBookingId;
        //}
    }
}
