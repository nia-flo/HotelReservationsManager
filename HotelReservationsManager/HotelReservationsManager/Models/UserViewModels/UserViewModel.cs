using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(string id, string userName, string email, string firstName, string middleName, string lastName, bool isActive)
        {
            Id = id;
            UserName = userName;
            Email = email;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            IsActive = isActive;
        }
    }
}
