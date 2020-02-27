using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Data.Models
{
    public class ClientReservation
    {
        public string Id { get; set; }

        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
