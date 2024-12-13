using HotelFlow.Data;
using HotelFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelFlow.Controllers
{
    public class PromoController : Controller
    {
        private readonly ApplicationDBContext _db;

        public PromoController(ApplicationDBContext db)
        {
            _db = db;
        }
        //public ActionResult ApplyPromoCode()
        //{
        //    return View();
        //}
        public ActionResult ApplyPromoCode(string code)
        {
            var promoCode = _db.PromoCodes
                .FirstOrDefault(p => p.Code == code && p.IsActive && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);

            if (promoCode != null)
            {
                decimal discountAmount = 0;

                if (promoCode.DiscountPercent.HasValue)
                {
                    discountAmount = promoCode.DiscountPercent.Value;
                }
                else
                {
                    discountAmount = promoCode.DiscountValue;
                }

                // ส่งส่วนลดให้แสดงผลในหน้าเว็บ
                ViewBag.Discount = discountAmount;
                ViewBag.Message = "โปรโมชั่นใช้งานได้";
            }
            else
            {
                ViewBag.Message = "โค้ดโปรโมชั่นไม่ถูกต้องหรือหมดอายุ";
            }

            return View();
        }

        // หน้าแสดงผลการสร้างโค้ดโปรโมชั่น
        public ActionResult CreatePromoCode()
        {
            return View();
        }

        // ฟังก์ชันสำหรับสร้างโค้ดโปรโมชั่น
        [HttpPost]
        public ActionResult CreatePromoCode(string code, decimal discountValue, decimal? discountPercent, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrEmpty(code) || (discountValue == 0 && discountPercent == 0) || startDate > endDate)
            {
                ViewBag.ErrorMessage = "กรุณากรอกข้อมูลให้ครบถ้วน";
                return View();
            }

            var promoCode = new PromoCode
            {
                Code = code,
                DiscountValue = discountValue,
                DiscountPercent = discountPercent,
                StartDate = startDate,
                EndDate = endDate,
                IsActive = true
            };

            _db.PromoCodes.Add(promoCode);
            _db.SaveChanges();

            ViewBag.SuccessMessage = "สร้างโค้ดโปรโมชั่นสำเร็จ";
            return View();
        }
    }
}
