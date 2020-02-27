using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.Reservation;
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
                Step = 1,
                CreatorId = userManager.GetUserId(User),
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                ChoosenClients = new List<ClientViewModel>(),
                ClientsSearch = new ClientSearchViewModel()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationCreateViewModel model)
        {
            if (model.Step == 1)
            {
                model.Step++;

                return View(model);
            }
            else if (model.Step == 2)
            {
                if (model.ClientsSearch.SearchBy == "FirstName")
                {
                    model.ClientsSearch.Clients =
                    context.Clients.Where(u => u.FirstName == model.ClientsSearch.Value)
                                   .Select(u => new ClientViewModel()
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName
                                   })
                                   .Where(c => !model.ChoosenClients.Contains(c))
                                   .ToList()
                                   .OrderBy(u => u.FirstName)
                                   .ThenBy(u => u.LastName)
                                   .ToList();
                }
                else if (model.ClientsSearch.SearchBy == "LastName")
                {
                    model.ClientsSearch.Clients =
                    context.Clients.Where(u => u.LastName == model.ClientsSearch.Value)
                                   .Select(u => new ClientViewModel()
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName
                                   })
                                   .Where(c => !model.ChoosenClients.Contains(c))
                                   .ToList()
                                   .OrderBy(u => u.FirstName)
                                   .ThenBy(u => u.LastName)
                                   .ToList();
                }
                else
                {
                    model.ClientsSearch.Clients = context.Clients
                                   .Select(u => new ClientViewModel()
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName
                                   })
                                   .Where(c => !model.ChoosenClients.Contains(c))
                                   .ToList()
                                   .OrderBy(u => u.FirstName)
                                   .ThenBy(u => u.LastName)
                                   .ToList();
                }

                return View(model);
            }

            Reservation reservation = new Reservation()
            {
                Id = Guid.NewGuid().ToString(),
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                //Clients = model.ChoosenClients.Select(c => context.Clients.Find(c.Id)).ToList(),
                Creator = context.Users.Find(model.CreatorId),
                IsAllInclusive = model.IsAllInclusive,
                IsBreakfastIncluded = model.IsBreakfastIncluded,
                Room = context.Rooms.Find(model.RoomId)
            };

            return Redirect("~/Reservation/Details/" + reservation.Id);
        }
    }
}