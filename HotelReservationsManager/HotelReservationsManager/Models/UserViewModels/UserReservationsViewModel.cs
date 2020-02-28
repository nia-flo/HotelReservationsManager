using HotelReservationsManager.Models.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.UserViewModels
{
    public class UserReservationsViewModel
    {
        public UserViewModel User { get; set; }
        public List<ReservationViewModel> Reservations { get; set; }

        public UserReservationsViewModel()
        {

        }

        public UserReservationsViewModel(UserViewModel user, List<ReservationViewModel> reservations)
        {
            User = user;
            Reservations = reservations;
        }
    }
}
