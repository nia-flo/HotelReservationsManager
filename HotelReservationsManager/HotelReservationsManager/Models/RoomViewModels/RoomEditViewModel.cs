using HotelReservationsManager.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.RoomViewModels
{
    public class RoomEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Range(1, 10000,
        ErrorMessage = "{0} must be between {1} and {2}.")]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        public RoomType Type { get; set; }

        public bool IsFree { get; set; }

        [Required]
        [Range(0, 10000,
        ErrorMessage = "{0} must be between {1} and {2}.")]
        [Display(Name = "Price for adults")]
        public decimal AdultPrice { get; set; }

        [Required]
        [Range(0, 10000,
        ErrorMessage = "{0} must be between {1} and {2}.")]
        [Display(Name = "Price for children")]
        public decimal ChildPrice { get; set; }

        [Required]
        [Range(1, 10000,
        ErrorMessage = "{0} must be between {1} and {2}.")]
        [Display(Name = "Room number")]
        public int Number { get; set; }
    }
}
