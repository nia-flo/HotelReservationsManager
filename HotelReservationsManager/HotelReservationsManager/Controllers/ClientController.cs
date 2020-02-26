using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models.ClientViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

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

            return Redirect("~/");
        }
    }
}