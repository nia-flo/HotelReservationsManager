using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Data.Models
{
    public class Reservation
    {
        public string Id { get; set; }

        public Room Room { get; set; }

        public User Creator { get; set; }

        public List<Client> Clients { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public bool IsBreakfastIncluded { get; set; }

        public bool IsAllInclusive { get; set; }
    }
}
