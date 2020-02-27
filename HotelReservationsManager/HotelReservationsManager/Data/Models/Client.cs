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

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }

        public virtual List<ClientReservation> ClientReservations { get; set; }

        public Client()
        {

        }

        public Client(string firstName, string lastName, string phoneNumber, string email, bool isAdult, List<ClientReservation> clientReservations)
        {
            Id = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            IsAdult = isAdult;
            ClientReservations = clientReservations;
        }
    }
}
