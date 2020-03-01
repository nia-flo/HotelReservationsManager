using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Data.Models.Enums;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.ClientViewModels;
using HotelReservationsManager.Models.ReservationViewModels;
using HotelReservationsManager.Models.RoomViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Controllers
{
    public class ReservationController : Controller
    {
        private DbContext context;
        private UserManager<User> userManager;

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

        public ReservationController(DbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Delete(string id)
        {
            Reservation reservation = context.Reservations.Find(id);

            foreach (var clientReservation in reservation.ClientReservations)
            {
                clientReservation.Client.ClientReservations.Remove(clientReservation);
                context.Update(clientReservation.Client);

                SendEmail(clientReservation.Client.Email, "Reservation cancelled", ReservationDetaisEmailBody(reservation));
            }

            context.Reservations.Remove(reservation);

            context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            List<ClientViewModel> clients = context.Clients.Select(c => new ClientViewModel(c.Id, c.FirstName,
                c.LastName, c.IsAdult))
                .ToList()
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();

            List<RoomViewModel> rooms = context.Rooms.Select(r => new RoomViewModel(r.Id, r.Capacity, r.Type,
                r.IsFree, r.Number))
                .ToList()
                .OrderBy(r => r.Number)
                .ToList();

            ReservationCreateViewModel model = new ReservationCreateViewModel(DateTime.Now, DateTime.Now, clients, rooms);

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationCreateViewModel model)
        {
            if (model.CheckInDate < DateTime.Now)
            {
                ModelState.AddModelError("CheckInDate", "Check-in date cannot be in the past.");
            }

            if (model.CheckOutDate < DateTime.Now)
            {
                ModelState.AddModelError("CheckOutDate", "Check-out date cannot be in the past.");
            }

            if (ModelState.IsValid)
            {
                foreach (var previousReservation in context.Reservations)
                {
                    if (previousReservation.Room.Id == model.ChoosenRoom
                        && ((previousReservation.CheckInDate <= model.CheckInDate
                            && model.CheckInDate < previousReservation.CheckOutDate) ||
                            (previousReservation.CheckInDate < model.CheckOutDate
                            && model.CheckInDate <= previousReservation.CheckOutDate)))
                    {
                        return Redirect("~/Reservation/RoomNotFree"); ;
                    }
                }

                Room room = context.Rooms.Find(model.ChoosenRoom);

                if (model.ChoosenClients.Count > room.Capacity)
                {
                    return Redirect("~/Reservation/TooManyPeople");
                }

                User creator = context.Users.Find(userManager.GetUserId(User));

                Reservation reservation = new Reservation(room, creator, new List<ClientReservation>(), model.CheckInDate, model.CheckOutDate, model.IsBreakfastIncluded, model.IsAllInclusive, 0);

                reservation.ClientReservations = model.ChoosenClients.Select(c => context.Clients.Find(c))
                    .Select(c => new ClientReservation(c.Id, c, reservation.Id, reservation)).ToList();

                foreach (var client in reservation.ClientReservations.Select(cr => cr.Client))
                {
                    if (client.IsAdult)
                    {
                        reservation.Price += reservation.Room.AdultPrice;
                    }
                    else
                    {
                        reservation.Price += reservation.Room.ChildPrice;
                    }
                }

                context.Reservations.Add(reservation);
                context.SaveChanges();

                string body = ReservationDetaisEmailBody(reservation);

                foreach (var client in reservation.ClientReservations.Select(cl => cl.Client.Email))
                {
                    SendEmail(client, "New hotel reservation", body);
                }

                return Redirect("~/Reservation/Details/" + reservation.Id);
            }

            model.Clients = context.Clients.Select(c => new ClientViewModel()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Id = c.Id,
                IsAdult = c.IsAdult
            }).ToList();

            model.Rooms = context.Rooms.Select(r => new RoomViewModel()
            {
                Id = r.Id,
                Capacity = r.Capacity,
                Type = r.Type,
                Number = r.Number

            }).ToList();

            return View(model);
        }

        public IActionResult RoomNotFree()
        {
            return View();
        }

        public IActionResult TooManyPeople()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            Reservation reservation = context.Reservations.Find(id);

            RoomViewModel room = new RoomViewModel(reservation.Room.Id, reservation.Room.Capacity,
                reservation.Room.Type, reservation.Room.IsFree, reservation.Room.Number);

            UserViewModel creator = new UserViewModel(reservation.Creator.Id, reservation.Creator.UserName,
                reservation.Creator.Email, reservation.Creator.FirstName, reservation.Creator.MiddleName,
                reservation.Creator.LastName, reservation.Creator.IsActive);

            List<ClientViewModel> clients = reservation.ClientReservations
                .Select(cr => new ClientViewModel(cr.Client.Id, cr.Client.FirstName,
                    cr.Client.LastName, cr.Client.IsAdult))
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();


            ReservationDetailsViewModel model = new ReservationDetailsViewModel(id, room, creator, clients,
                reservation.CheckInDate, reservation.CheckOutDate, reservation.IsBreakfastIncluded,
                reservation.IsAllInclusive, reservation.Price);

            return View(model);
        }

        public IActionResult Search(ReservationSearchViewModel model)
        {
            if (model.SearchBy == "RoomType")
            {
                model.Reservations = context.Reservations.Where(u => u.Room.Type == (RoomType)int.Parse(model.Value))
                               .ToList()
                               .OrderBy(u => u.Room.Number)
                               .ThenBy(u => u.Room.Type)
                               .Select(u => new ReservationViewModel(u.Id, u.Room.Number, u.Room.Type, u.CheckInDate,
                                    u.CheckOutDate, u.Price))
                               .ToList();
            }
            else if (model.SearchBy == "RoomNumber")
            {
                model.Reservations = context.Reservations.Where(u => u.Room.Number == int.Parse(model.Value))
                               .ToList()
                               .OrderBy(u => u.Room.Number)
                               .ThenBy(u => u.Room.Type)
                               .Select(u => new ReservationViewModel(u.Id, u.Room.Number, u.Room.Type, u.CheckInDate,
                                    u.CheckOutDate, u.Price))
                               .ToList();
            }
            else
            {
                model.Reservations = context.Reservations
                                               .OrderBy(u => u.Room.Number)
                                               .ThenBy(u => u.Room.Type)
                                               .Select(u => new ReservationViewModel(u.Id, u.Room.Number, u.Room.Type, u.CheckInDate,
                                                    u.CheckOutDate, u.Price))
                                               .ToList();
            }

            return View(model);
        }



        [HttpGet]
        public IActionResult Edit(string id)
        {
            Reservation reservation = context.Reservations.FindAsync(id).Result;

            List<string> choosenClients = reservation.ClientReservations.Select(cr => cr.Client.Id).ToList();

            List<ClientViewModel> clients = context.Clients.Select(c => new ClientViewModel(c.Id, c.FirstName,
                c.LastName, c.IsAdult))
                .ToList()
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();

            List<RoomViewModel> rooms = context.Rooms.Select(r => new RoomViewModel(r.Id, r.Capacity, r.Type,
                r.IsFree, r.Number))
                .ToList()
                .OrderBy(r => r.Number)
                .ToList();

            ReservationEditViewModel model = new ReservationEditViewModel(id, reservation.CheckInDate,
                reservation.CheckOutDate, reservation.IsBreakfastIncluded, reservation.IsAllInclusive,
                choosenClients, clients, rooms, reservation.Room.Id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ReservationEditViewModel model)
        {
            if (model.CheckInDate < DateTime.Now)
            {
                ModelState.AddModelError("CheckInDate", "Check-in date cannot be in the past.");
            }

            if (model.CheckOutDate < DateTime.Now)
            {
                ModelState.AddModelError("CheckOutDate", "Check-out date cannot be in the past.");
            }

            if (ModelState.IsValid)
            {
                Reservation reservation = context.Reservations.FindAsync(model.Id).Result;

                foreach (var previousReservation in context.Reservations)
                {
                    if (previousReservation.Id != reservation.Id &&
                        previousReservation.Room.Id == model.ChoosenRoom
                        && ((previousReservation.CheckInDate <= model.CheckInDate
                            && model.CheckInDate < previousReservation.CheckOutDate) ||
                            (previousReservation.CheckInDate < model.CheckOutDate
                            && model.CheckInDate <= previousReservation.CheckOutDate)))
                    {
                        return Redirect("~/Reservation/RoomNotFree");
                        //ModelState.AddModelError("ChoosenRoom", "There is already created room with this number.");
                    }
                }

                Room room = context.Rooms.Find(model.ChoosenRoom);

                if (model.ChoosenClients.Count > room.Capacity)
                {
                    return Redirect("~/Reservation/TooManyPeople");
                }

                User creator = context.Users.Find(userManager.GetUserId(User));

                //Reservation reservation = new Reservation(room, creator, new List<ClientReservation>(), model.CheckInDate, model.CheckOutDate, model.IsBreakfastIncluded, model.IsAllInclusive, 0);

                reservation.Room = room;
                reservation.CheckInDate = model.CheckInDate;
                reservation.CheckOutDate = model.CheckOutDate;
                reservation.IsBreakfastIncluded = model.IsBreakfastIncluded;
                reservation.IsAllInclusive = model.IsAllInclusive;

                ClientReservation[] clientReservations = new ClientReservation[reservation.ClientReservations.Count];
                reservation.ClientReservations.CopyTo(clientReservations);

                foreach (var clientReservation in clientReservations)
                {
                    reservation.ClientReservations.Remove(clientReservation);
                    clientReservation.Client.ClientReservations.Remove(clientReservation);
                    context.ClientReservation.Remove(clientReservation);
                }

                reservation.ClientReservations = model.ChoosenClients.Select(c => context.Clients.Find(c))
                    .Select(c => new ClientReservation(c.Id, c, reservation.Id, reservation)).ToList();

                reservation.Price = 0;
                foreach (var client in reservation.ClientReservations.Select(cr => cr.Client))
                {
                    if (client.IsAdult)
                    {
                        reservation.Price += reservation.Room.AdultPrice;
                    }
                    else
                    {
                        reservation.Price += reservation.Room.ChildPrice;
                    }
                }

                context.Update(reservation);
                context.SaveChanges();

                StringBuilder body = new StringBuilder();
                body.AppendLine("<h1>Reservation details:</h1><br/>");
                body.AppendLine($"<h3>Check-in date:</h3> {reservation.CheckInDate.Date}<br/>");
                body.AppendLine($"<h3>Check-out date:</h3> {reservation.CheckOutDate.Date}<br/>");
                if (reservation.IsBreakfastIncluded)
                {
                    body.AppendLine("<h3>Extra:</h3> With included breakfast.<br/>");
                    body.AppendLine();
                }
                if (reservation.IsAllInclusive)
                {
                    body.AppendLine("<h3>Extra:</h3> With all-inclusive.<br/>");
                    body.AppendLine();
                }
                body.AppendLine("<h3>Clients:</h3><ul>");
                foreach (var client in reservation.ClientReservations.Select(cl => cl.Client))
                {
                    body.AppendLine($"<li> {client.FirstName} {client.LastName}</li>");
                }
                body.AppendLine("</ul>");
                body.AppendLine("<h3>Room:</h3>");
                if (room.Type == RoomType.TwoBeds)
                {
                    body.AppendLine($"Room No. {reservation.Room.Number} - Room with separate beds<br/>");
                }
                else if (room.Type == RoomType.DoubleBed)
                {
                    body.AppendLine($"Room No. {reservation.Room.Number} - Room with a double bed<br/>");
                }
                else if (room.Type == RoomType.PentHouse)
                {
                    body.AppendLine($"Room No. {reservation.Room.Number} - Penthouse<br/>");
                }
                else
                {
                    body.AppendLine($"Room No. {reservation.Room.Number} - {reservation.Room.Type}<br/>");
                }
                body.AppendLine($"<h3>Price:</h3> {reservation.Price}");


                foreach (var client in reservation.ClientReservations.Select(cl => cl.Client.Email))
                {
                    SendEmail(client, "Hotel reservation edited", body.ToString());
                }

                return Redirect("~/Reservation/Details/" + reservation.Id);
            }

            model.Clients = context.Clients.Select(c => new ClientViewModel()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Id = c.Id,
                IsAdult = c.IsAdult
            }).ToList();

            model.Rooms = context.Rooms.Select(r => new RoomViewModel()
            {
                Id = r.Id,
                Capacity = r.Capacity,
                Type = r.Type,
                Number = r.Number

            }).ToList();

            return View(model);
        }
    }
}