using HotelReservationsManager.Attributes;
using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.RoomViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.ReservationViewModels
{
    public class ReservationEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Check-in date")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DateGreaterThan("CheckInDate")]
        [Display(Name = "Check-out date")]
        public DateTime CheckOutDate { get; set; }

        public bool IsBreakfastIncluded { get; set; }

        public bool IsAllInclusive { get; set; }

        [Required]
        [Display(Name = "Clients")]
        public List<string> ChoosenClients { get; set; }

        public List<ClientViewModel> Clients { get; set; }

        public List<RoomViewModel> Rooms { get; set; }

        [Required]
        [Display(Name = "Room")]
        public string ChoosenRoom { get; set; }

        public ReservationEditViewModel()
        {

        }

        public ReservationEditViewModel(string id, DateTime checkInDate, DateTime checkOutDate, bool isBreakfastIncluded, bool isAllInclusive, List<string> choosenClients, List<ClientViewModel> clients, List<RoomViewModel> rooms, string choosenRoom)
        {
            Id = id;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            IsBreakfastIncluded = isBreakfastIncluded;
            IsAllInclusive = isAllInclusive;
            ChoosenClients = choosenClients;
            Clients = clients;
            Rooms = rooms;
            ChoosenRoom = choosenRoom;
        }
    }
}
