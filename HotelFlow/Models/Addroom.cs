using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace HotelFlow.Models
{
    public class Addroom
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }  // ใช้ decimal แทน int
        public string ImgUrl { get; set; }
        //public int IsAvailable { get; set; }
        //public string BookingId { get; set; }
        //public DateTime CheckIn { get; set; }
        //public DateTime CheckOut { get; set; }
        //public string BookingStatus { get; set; }
        //public string CustomerName { get; set; }
        //public string Username { get; set; }
        
    }
}
