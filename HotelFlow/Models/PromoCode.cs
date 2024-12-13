namespace HotelFlow.Models
{
    public class PromoCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; } // ส่วนลดในรูปแบบของค่าเต็ม
        public decimal? DiscountPercent { get; set; } // ส่วนลดในรูปแบบของเปอร์เซ็นต์
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
