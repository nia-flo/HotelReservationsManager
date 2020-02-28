using HotelReservationsManager.Models.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ClientViewModels
{
    public class ClientReservationsViewModel
    {
        public ClientViewModel Client { get; set; }
        public List<ReservationViewModel> Reservations { get; set; }

        public ClientReservationsViewModel()
        {

        }

        public ClientReservationsViewModel(ClientViewModel client, List<ReservationViewModel> reservations)
        {
            Client = client;
            Reservations = reservations;
        }
    }
}
