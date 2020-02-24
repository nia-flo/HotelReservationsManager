using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Data.Models
{
    public class Client
    {
        public String Id { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }
    }
}
