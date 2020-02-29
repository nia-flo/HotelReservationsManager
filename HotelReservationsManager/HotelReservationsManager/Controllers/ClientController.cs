using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Data.Models.Enums;
using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.ReservationViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace HotelReservationsManager.Controllers
{
    public class ClientController : Controller
    {
        private DbContext context;

        private string ReservationDetaisEmailBody(Reservation reservation)
        {
            StringBuilder body = new StringBuilder();

            body.AppendLine("<h1>Reservation details:</h1><br/>");

            body.AppendLine($"<h3>Check-in date:</h3> {reservation.CheckInDate.Date}<br/>");

            body.AppendLine($"<h3>Check-out date:</h3> {reservation.CheckOutDate.Date}<br/>");

            if (reservation.IsBreakfastIncluded || reservation.IsAllInclusive)
            {
                body.AppendLine("<h3>Extras:</h3>");
                if (reservation.IsBreakfastIncluded)
                {
                    body.AppendLine("<li> With included breakfast.</li>");
                }
                if (reservation.IsAllInclusive)
                {
                    body.AppendLine("<li> With all-inclusive.</li>");
                }
            }

            body.AppendLine("<h3>Clients:</h3>");
            foreach (var client in reservation.ClientReservations.Select(cl => cl.Client))
            {
                body.AppendLine($"<li> {client.FirstName} {client.LastName}</li>");
            }

            body.AppendLine("<h3>Room:</h3>");
            if (reservation.Room.Type == RoomType.TwoBeds)
            {
                body.AppendLine($"Room No. {reservation.Room.Number} - Room with separate beds<br/>");
            }
            else if (reservation.Room.Type == RoomType.DoubleBed)
            {
                body.AppendLine($"Room No. {reservation.Room.Number} - Room with a double bed<br/>");
            }
            else if (reservation.Room.Type == RoomType.PentHouse)
            {
                body.AppendLine($"Room No. {reservation.Room.Number} - Penthouse<br/>");
            }
            else
            {
                body.AppendLine($"Room No. {reservation.Room.Number} - {reservation.Room.Type}<br/>");
            }

            body.AppendLine($"<h3>Price:</h3> {reservation.Price}");

            return body.ToString();
        }

        protected void SendEmail(string toEmail, string subject, string body)
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress("hotel.reservations.manager@gmail.com", "Hotel Reservations Manager", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("hotel.reservations.manager@gmail.com", "hotelreservationsmanagerpassword");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            client.Send(mail);
        }

        public ClientController(DbContext context)
        {
            this.context = context;
        }

        public IActionResult Delete(string id)
        {
            Client client = context.Clients.First(context => context.Id == id);

            List<ClientReservation> clientReservations = client.ClientReservations;

            foreach (var clientReservation in clientReservations)
            {
                context.ClientReservation.Remove(clientReservation);

                Reservation reservation = clientReservation.Reservation;

                if (reservation.ClientReservations.Count == 1)
                {
                    context.Reservations.Remove(reservation);

                    SendEmail(client.Email, "Reservation cancelled", ReservationDetaisEmailBody(reservation));
                }
                else
                {
                    reservation.ClientReservations.Remove(clientReservation);
                    context.Reservations.Update(reservation);
                }
                
            }

            context.Clients.Remove(client);

            context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ClientCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(ClientCreateViewModel model)
        {
            if (context.Clients.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber) != null)
            {
                ModelState.AddModelError("PhoneNumber", "There is an user with this phone number.");
                //return Page();
            }

            if (context.Clients.FirstOrDefault(u => u.Email == model.Email) != null)
            {
                ModelState.AddModelError("Email", "There is an user with this email.");
                //return Page();
            }

            if (ModelState.IsValid)
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

            return View(model);
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
            Client samePhoneNumber = context.Clients.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
            if (samePhoneNumber != null && samePhoneNumber.Id != model.Id)
            {
                ModelState.AddModelError("PhoneNumber", "There is an user with this phone number.");
                //return Page();
            }

            Client sameEmail = context.Clients.FirstOrDefault(u => u.Email == model.Email);
            if (sameEmail != null && sameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "There is an user with this email.");
                //return Page();
            }

            if (ModelState.IsValid)
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

            return View(model);
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