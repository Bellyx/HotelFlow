using HotelFlow.Data;
using HotelFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


namespace HotelFlow.Controllers
{
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
            //var status = roomTable.Select(r => r.BookingStatus ==); 

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

        // GET: Dashboard/Create
        public ActionResult Create()
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
    }
}
