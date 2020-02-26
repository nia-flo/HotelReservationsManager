using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Data.Models.Enums;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.RoomViewModels;
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
            RoomCreateViewModel model = new RoomCreateViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(RoomCreateViewModel model)
        {
            if (ModelState.IsValid && context.Rooms.FirstOrDefault(r => r.Number == model.Number) == null)
            {
                Room room = new Room()
                {
                    Id = Guid.NewGuid().ToString(),
                    Capacity = model.Capacity,
                    Type = (RoomType)model.Type,
                    AdultPrice = model.AdultPrice,
                    ChildPrice = model.ChildPrice,
                    Number = model.Number,
                    IsFree = true
                };

                context.Rooms.AddAsync(room);
                context.SaveChanges();

                return Redirect("~/Room/Details/" + room.Id);
            }

            return View(model);
        }

        public IActionResult Details(string id)
        {
            Room room = context.Rooms.FindAsync(id).Result;

            RoomDetailsViewModel model = new RoomDetailsViewModel()
            {
                Id = room.Id,
                Capacity = room.Capacity,
                Type = room.Type,
                AdultPrice = room.AdultPrice,
                ChildPrice = room.ChildPrice,
                Number = room.Number,
                IsFree = room.IsFree
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Room room = context.Rooms.FindAsync(id).Result;

            RoomEditViewModel model = new RoomEditViewModel()
            {
                Id = room.Id,
                Capacity = room.Capacity,
                Type = room.Type,
                AdultPrice = room.AdultPrice,
                ChildPrice = room.ChildPrice,
                Number = room.Number,
                IsFree = room.IsFree
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(RoomEditViewModel model)
        {
            Room room = context.Rooms.FindAsync(model.Id).Result;

            room.Capacity = model.Capacity;
            room.Type = model.Type;
            room.AdultPrice = model.AdultPrice;
            room.ChildPrice = model.ChildPrice;
            room.Number = model.Number;
            room.IsFree = model.IsFree;

            context.Update(room);
            context.SaveChanges();

            return Redirect("~/Room/Details/" + room.Id);
        }
    }
}