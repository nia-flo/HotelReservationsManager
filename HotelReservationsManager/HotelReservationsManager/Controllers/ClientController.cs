using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.ReservationViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationsManager.Controllers
{
    public class ClientController : Controller
    {
        private DbContext context;

        public ClientController(DbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ClientCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(ClientCreateViewModel model)
        {
            Client client = new Client()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                IsAdult = !model.IsAdult
            };

            context.Clients.AddAsync(client);
            context.SaveChanges();

            return Redirect("~/Client/Details/" + client.Id);
        }

        public IActionResult Details(string id)
        {
            Client client = context.Clients.First(context => context.Id == id);

            ClientDetailsViewModel model = new ClientDetailsViewModel()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                IsAdult = client.IsAdult
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Client client = context.Clients.FindAsync(id).Result;

            ClientEditViewModel model = new ClientEditViewModel()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                IsAdult = client.IsAdult
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ClientEditViewModel model)
        {
            Client client = context.Clients.FindAsync(model.Id).Result;

            client.FirstName = model.FirstName;
            client.LastName = model.LastName;
            client.PhoneNumber = model.PhoneNumber;
            client.Email = model.Email;
            client.IsAdult = !model.IsAdult;

            context.Clients.Update(client);
            context.SaveChanges();

            return Redirect("~/Client/Details/" + client.Id);
        }

        public IActionResult Search(ClientSearchViewModel model)
        {
            if (model.SearchBy == "FirstName")
            {
                model.Clients =
                context.Clients.Where(u => u.FirstName == model.Value)
                               .ToList()
                               .OrderBy(u => u.FirstName)
                               .ThenBy(u => u.LastName)
                               .Select(u => new ClientViewModel()
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   IsAdult = u.IsAdult
                               })
                               .ToList();
            }
            else if (model.SearchBy == "LastName")
            {
                model.Clients =
                context.Clients.Where(u => u.LastName == model.Value)
                               .ToList()
                               .OrderBy(u => u.FirstName)
                               .ThenBy(u => u.LastName)
                               .Select(u => new ClientViewModel()
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   IsAdult = u.IsAdult
                               })
                               .ToList();
            }
            else
            {
                model.Clients = context.Clients
                               .OrderBy(u => u.FirstName)
                               .ThenBy(u => u.LastName)
                               .Select(u => new ClientViewModel()
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   IsAdult = u.IsAdult
                               })
                               .ToList();
            }

            return View(model);
        }

        public IActionResult Reservations(string id)
        {
            Client client = context.Clients.Find(id);

            ClientViewModel clientViewModel = new ClientViewModel(client.Id, client.FirstName, client.LastName,
                client.IsAdult);

            List<ReservationViewModel> reservations = context.ClientReservation.Where(cl => cl.Client.Id == id)
                .Select(r => new ReservationViewModel(r.Reservation.Id, r.Reservation.Room.Number,
                    r.Reservation.Room.Type, r.Reservation.CheckInDate, r.Reservation.CheckOutDate, r.Reservation.Price))
                .ToList();

            ClientReservationsViewModel model = new ClientReservationsViewModel(clientViewModel, reservations);

            return View(model);
        }
    }
}