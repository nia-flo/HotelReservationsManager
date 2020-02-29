using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

        public IActionResult Delete(string id)
        {
            Room room = context.Rooms.FindAsync(id).Result;

            IEnumerable<Reservation> reservations = context.Reservations.Where(r => r.Room.Id == room.Id);

            foreach (var reservation in reservations)
            {
                IEnumerable<ClientReservation> clientReservations = reservation.ClientReservations;

                foreach (var clientReservation in clientReservations)
                {
                    clientReservation.Client.ClientReservations.Remove(clientReservation);
                    context.Update(clientReservation.Client);

                    SendEmail(clientReservation.Client.Email, "Reservation cancelled", ReservationDetaisEmailBody(reservation));

                    context.ClientReservation.Remove(clientReservation);
                }
            }

            context.Rooms.Remove(room);

            context.SaveChanges();

            return View();
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
            if (context.Rooms.FirstOrDefault(r => r.Number == model.Number) != null)
            {
                ModelState.AddModelError("Number", "There is already created room with this number.");
            }

            if (ModelState.IsValid)
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

            string type;
            if (room.Type == RoomType.TwoBeds)
            {
                type = "Room with separate beds";
            }
            else if (room.Type == RoomType.DoubleBed)
            {
                type = "Room with a double bed";
            }
            else if (room.Type == RoomType.PentHouse)
            {
                type = "Penthouse";
            }
            else if (room.Type == RoomType.Maisonette)
            {
                type = "Maisonette";
            }
            else
            {
                type = "Apartment";
            }

            RoomDetailsViewModel model = new RoomDetailsViewModel()
            {
                Id = room.Id,
                Capacity = room.Capacity,
                Type = type,
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

            if (room.Number != model.Number && context.Rooms.First(r => r.Number == model.Number) != null)
            {
                ModelState.AddModelError("Number", "There is already created room with this number.");

            }
            if (ModelState.IsValid)
            {

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

            return View();
        }

        public IActionResult Search(RoomSearchViewModel model)
        {
            List<Room> updatedRooms = new List<Room>();
            foreach (var room in context.Rooms)
            {
                room.IsFree = true;
                foreach (var reservation in context.Reservations)
                {
                    if (reservation.Room.Id == room.Id &&
                        reservation.CheckInDate <= DateTime.Now && DateTime.Now <= reservation.CheckOutDate)
                    {
                        room.IsFree = false;
                        break;
                    }
                }

                updatedRooms.Add(room);
            }

            foreach (var room in updatedRooms)
            {
                context.Update(room);
            }
            context.SaveChanges();

            if (model.SearchBy == "Capacity")
            {
                model.Rooms = context.Rooms.Where(u => u.Capacity == int.Parse(model.Value))
                               .ToList()
                               .OrderBy(u => u.Number)
                               .ThenBy(u => u.Type)
                               .Select(u => new RoomViewModel()
                               {
                                   Id = u.Id,
                                   Capacity = u.Capacity,
                                   Number = u.Number,
                                   IsFree = u.IsFree,
                                   Type = u.Type
                               })
                               .ToList();
            }
            else if (model.SearchBy == "Type")
            {
                model.Rooms = context.Rooms.Where(u => u.Type == (RoomType)int.Parse(model.Value))
                               .ToList()
                               .OrderBy(u => u.Number)
                               .ThenBy(u => u.Type)
                               .Select(u => new RoomViewModel()
                               {
                                   Id = u.Id,
                                   Capacity = u.Capacity,
                                   Number = u.Number,
                                   IsFree = u.IsFree,
                                   Type = u.Type
                               })
                               .ToList();
            }
            else if (model.SearchBy == "IsFree")
            {
                bool isFree = (model.Value == "Free");
                model.Rooms = context.Rooms.Where(u => u.IsFree == isFree)
                               .ToList()
                               .OrderBy(u => u.Number)
                               .ThenBy(u => u.Type)
                               .Select(u => new RoomViewModel()
                               {
                                   Id = u.Id,
                                   Capacity = u.Capacity,
                                   Number = u.Number,
                                   IsFree = u.IsFree,
                                   Type = u.Type
                               })
                               .ToList();
            }
            else
            {
                model.Rooms = context.Rooms
                               .OrderBy(u => u.Number)
                               .ThenBy(u => u.Type)
                               .Select(u => new RoomViewModel()
                               {
                                   Id = u.Id,
                                   Capacity = u.Capacity,
                                   Number = u.Number,
                                   IsFree = u.IsFree,
                                   Type = u.Type
                               })
                               .ToList();
            }

            return View(model);
        }
    }
}