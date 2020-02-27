using HotelReservationsManager.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.RoomViewModels
{
    public class RoomViewModel
    {
        public string Id { get; set; }

        public int Capacity { get; set; }

        public RoomType Type { get; set; }

        public bool IsFree { get; set; }

        public int Number { get; set; }

        public RoomViewModel()
        {

        }

        public RoomViewModel(string id, int capacity, RoomType type, bool isFree, int number)
        {
            Id = id;
            Capacity = capacity;
            Type = type;
            IsFree = isFree;
            Number = number;
        }
    }
}
