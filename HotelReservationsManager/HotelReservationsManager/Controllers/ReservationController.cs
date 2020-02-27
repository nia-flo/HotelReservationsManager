using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.Reservation;
using HotelReservationsManager.Models.RoomViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Controllers
{
    public class ReservationController : Controller
    {
        private DbContext context;
        private UserManager<User> userManager;

        public ReservationController(DbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ReservationCreateViewModel model = new ReservationCreateViewModel()
            {
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                ChoosenClients = new List<string>(),
                Clients = context.Clients.Select(c => new ClientViewModel()
                                            {
                                                FirstName = c.FirstName,
                                                LastName = c.LastName,
                                                Id = c.Id,
                                                IsAdult = c.IsAdult
                                            }).ToList(),
                Rooms = context.Rooms.Select(r => new RoomViewModel()
                                            {
                                                Id = r.Id,
                                                Capacity = r.Capacity,
                                                Type = r.Type,
                                                Number = r.Number

                                            }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationCreateViewModel model)
        {
            Room room = context.Rooms.Find(model.ChoosenRoom);

            //TODO: check if the room is free in this period

            User creator = context.Users.Find(userManager.GetUserId(User));

            Reservation reservation = new Reservation(room, creator, new List<ClientReservation>(), model.CheckInDate, model.CheckOutDate, model.IsBreakfastIncluded, model.IsAllInclusive);

            reservation.ClientReservations = model.ChoosenClients.Select(c => context.Clients.Find(c))
                .Select(c => new ClientReservation(c.Id, c, reservation.Id, reservation)).ToList();


            context.Reservations.Add(reservation);
            context.SaveChanges();

            return Redirect("~/Reservation/Details/" + reservation.Id);
        }
    }
}