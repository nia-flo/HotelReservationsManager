using HotelReservationsManager.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models
{
    public class RoomDetailsViewModel
    {
        public string Id { get; set; }

        public int Capacity { get; set; }

        public RoomType Type { get; set; }

        public bool IsFree { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildPrice { get; set; }

        public int Number { get; set; }
    }
}
