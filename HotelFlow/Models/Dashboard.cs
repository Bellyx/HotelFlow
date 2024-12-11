using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelFlow.Models
{
    public class Dashboard
    {
        public int RoomId { get; set; }
        public decimal Price { get; set; }  // ใช้ decimal แทน int
        public int Capacity { get; set; }
        public int IsAvailable { get; set; }
        public string BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string BookingStatus { get; set; }
        public string CustomerName { get; set; }
        public string Username { get; set; }
        public string RoomType { get; set; }

    }
}

