using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Controllers
{
    public class RoomController : Controller
    {
        private DbContext context;

        public RoomController(DbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateRoomViewModel model = new CreateRoomViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = new Room()
                {
                    Id = Guid.NewGuid().ToString(),
                    Capacity = model.Capacity,
                    Type = model.Type,
                    AdultPrice = model.AdultPrice,
                    ChildPrice = model.ChildPrice,
                    Number = model.Number,
                    IsFree = true
                };

                context.Rooms.AddAsync(room);
                context.SaveChanges();

                RoomViewModel roomViewModel = new RoomViewModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Capacity = room.Capacity,
                    Type = room.Type,
                    AdultPrice = room.AdultPrice,
                    ChildPrice = room.ChildPrice,
                    Number = room.Number,
                    IsFree = room.IsFree
                };

                return RoomDetails(roomViewModel);
            }

            return View(model);
        }

        public IActionResult RoomDetails(RoomViewModel model)
        {
            return View(model);
        }
    }
}