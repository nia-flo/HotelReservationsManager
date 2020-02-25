using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.UserViewModels
{
    public class UsersByUsernameViewModel
    {
        public string UserName { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}
