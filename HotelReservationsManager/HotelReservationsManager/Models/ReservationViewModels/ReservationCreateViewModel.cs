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
    public class ReservationCreateViewModel
    {
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
    }
}
