using HotelReservationsManager.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ReservationViewModels
{
    public class ReservationViewModel
    {
        public string Id { get; set; }

        public int RoomNumber { get; set; }

        public RoomType RoomType { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal Price { get; set; }

        public ReservationViewModel()
        {

        }

        public ReservationViewModel(string id, int roomNumber, RoomType roomType, DateTime checkInDate, DateTime checkOutDate, decimal price)
        {
            Id = id;
            RoomNumber = roomNumber;
            RoomType = roomType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Price = price;
        }
    }
}
