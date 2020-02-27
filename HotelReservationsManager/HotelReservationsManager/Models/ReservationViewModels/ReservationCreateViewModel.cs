using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.RoomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ReservationViewModels
{
    public class ReservationCreateViewModel
    {
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public bool IsBreakfastIncluded { get; set; }

        public bool IsAllInclusive { get; set; }

        public List<string> ChoosenClients { get; set; }

        public List<ClientViewModel> Clients { get; set; }

        public List<RoomViewModel> Rooms { get; set; }

        public string ChoosenRoom { get; set; }
    }
}
