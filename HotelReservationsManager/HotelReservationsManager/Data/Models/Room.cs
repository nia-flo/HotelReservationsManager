﻿using HotelReservationsManager.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Data.Models
{
    public class Room
    {
        public string Id { get; set; }

        public int Capacity { get; set; }

        public RoomType Type { get; set; }

        public bool IsFree { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildPrice { get; set; }

        public int Number { get; set; }

        public Room()
        {

        }

        public Room(int capacity, RoomType type, bool isFree, decimal adultPrice, decimal childPrice, int number)
        {
            Id = Guid.NewGuid().ToString();
            Capacity = capacity;
            Type = type;
            IsFree = isFree;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Number = number;
        }
    }
}
