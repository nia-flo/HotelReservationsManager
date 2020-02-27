using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.RoomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservation
{
    public class ReservationDetailsViewModel
    {
        public string Id { get; set; }

        public virtual RoomViewModel Room { get; set; }

        public virtual UserViewModel Creator { get; set; }

        public virtual List<ClientViewModel> Clients { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public bool IsBreakfastIncluded { get; set; }

        public bool IsAllInclusive { get; set; }

        public decimal Price { get; set; }

        public ReservationDetailsViewModel()
        {

        }
        public ReservationDetailsViewModel(string id, RoomViewModel room, UserViewModel creator, List<ClientViewModel> clients, DateTime checkInDate, DateTime checkOutDate, bool isBreakfastIncluded, bool isAllInclusive, decimal price)
        {
            Id = id;
            Room = room;
            Creator = creator;
            Clients = clients;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            IsBreakfastIncluded = isBreakfastIncluded;
            IsAllInclusive = isAllInclusive;
            Price = price;
        }
    }
}
