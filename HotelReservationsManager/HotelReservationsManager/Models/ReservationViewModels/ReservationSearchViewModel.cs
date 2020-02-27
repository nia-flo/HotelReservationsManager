using HotelReservationsManager.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ReservationViewModels
{
    public class ReservationSearchViewModel
    {
        public string SearchBy { get; set; }

        public string Value { get; set; }

        public List<ReservationViewModel> Reservations { get; set; }

        public ReservationSearchViewModel()
        {

        }

        public ReservationSearchViewModel(List<ReservationViewModel> reservations)
        {
            Reservations = reservations;
        }
    }
}
