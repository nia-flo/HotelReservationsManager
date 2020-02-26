using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ClientViewModels
{
    public class ClientSearchViewModel
    {
        public List<ClientViewModel> Clients { get; set; }

        public string SearchBy { get; set; }

        public string Value { get; set; }
    }
}
