using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.RoomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservation
{
    public class ReservationCreateViewModel
    {
        public int Step { get; set; }

        //Step1

        public string CreatorId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public bool IsBreakfastIncluded { get; set; }

        public bool IsAllInclusive { get; set; }

        //Step2

        public List<ClientViewModel> ChoosenClients { get; set; }

        public ClientSearchViewModel ClientsSearch { get; set; }

        //Step3

        public string RoomId { get; set; }
    }
}
