using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ClientViewModels
{
    public class ClientViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAdult { get; set; }

        public ClientViewModel()
        {

        }

        public ClientViewModel(string id, string firstName, string lastName, bool isAdult)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsAdult = isAdult;
        }
    }
}
