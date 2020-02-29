using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.RoomViewModels
{
    public class RoomSearchViewModel
    {
        public string SearchBy { get; set; }

        public string Value { get; set; }

        public List<RoomViewModel> Rooms { get; set; }

        public RoomSearchViewModel()
        {

        }

        public RoomSearchViewModel(List<RoomViewModel> rooms)
        {
            Rooms = rooms;
        }
    }
}
