using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.UserViewModels
{
    public class SearchEmployeesViewModel
    {
        public string SearchBy { get; set; }

        public string Value { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}
