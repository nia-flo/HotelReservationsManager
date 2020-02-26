using HotelReservationsManager.Data.Models.Enums;

namespace HotelReservationsManager.Models
{
    public class RoomCreateViewModel
    {
        public int Capacity { get; set; }

        public RoomType Type { get; set; }

        public bool IsFree { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildPrice { get; set; }

        public int Number { get; set; }
    }
}
