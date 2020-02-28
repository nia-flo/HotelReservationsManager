using System;
using System.Collections.Generic;
using System.Linq;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.ReservationViewModels;
using HotelReservationsManager.Models.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Controllers
{
    public class UserController : Controller
    {
        private DbContext context;

        public UserController(DbContext context)
        {
            this.context = context;
        }

        public IActionResult Search(SearchEmployeesViewModel model)
        {
            if (model.SearchBy == "Username")
            {
                model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.UserName == model.Value)
                             .ToList()
                             .OrderBy(u => u.FirstName)
                             .ThenBy(u => u.MiddleName)
                             .ThenBy(u => u.LastName)
                             .Select(u => new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 MiddleName = u.MiddleName,
                                 LastName = u.LastName,
                                 IsActive = u.IsActive
                             })
                             .ToList();
            }
            else if (model.SearchBy == "FirstName")
            {
                model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.FirstName == model.Value)
                             .ToList()
                             .OrderBy(u => u.FirstName)
                             .ThenBy(u => u.MiddleName)
                             .ThenBy(u => u.LastName)
                             .Select(u => new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 MiddleName = u.MiddleName,
                                 LastName = u.LastName,
                                 IsActive = u.IsActive
                             })
                             .ToList();
            }
            else if (model.SearchBy == "MiddleName")
            {
                model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.MiddleName == model.Value)
                             .ToList()
                             .OrderBy(u => u.FirstName)
                             .ThenBy(u => u.MiddleName)
                             .ThenBy(u => u.LastName)
                             .Select(u => new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 MiddleName = u.MiddleName,
                                 LastName = u.LastName,
                                 IsActive = u.IsActive
                             })
                             .ToList();
            }
            else if (model.SearchBy == "LastName")
            {
                model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.LastName == model.Value)
                             .ToList()
                             .OrderBy(u => u.FirstName)
                             .ThenBy(u => u.MiddleName)
                             .ThenBy(u => u.LastName)
                             .Select(u => new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 MiddleName = u.MiddleName,
                                 LastName = u.LastName,
                                 IsActive = u.IsActive
                             })
                             .ToList();
            }
            else if(model.SearchBy == "Email")
            {
                model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.Email == model.Value)
                             .ToList()
                             .OrderBy(u => u.FirstName)
                             .ThenBy(u => u.MiddleName)
                             .ThenBy(u => u.LastName)
                             .Select(u => new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 MiddleName = u.MiddleName,
                                 LastName = u.LastName,
                                 IsActive = u.IsActive
                             })
                             .ToList();
            }
            else
            {
                model.Users = context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .ToList()
                             .OrderBy(u => u.FirstName)
                             .ThenBy(u => u.MiddleName)
                             .ThenBy(u => u.LastName)
                             .Select(u => new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 MiddleName = u.MiddleName,
                                 LastName = u.LastName,
                                 IsActive = u.IsActive
                             })
                             .ToList();
            }

            return View(model);
        }

        public IActionResult Details(string id)
        {
            User user = context.Users.First(u => u.Id == id);

            UserDetailsViewModel model = new UserDetailsViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                EGN = user.EGN,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DismissDate = user.DismissDate,
                HireDate = user.HireDate,
                IsActive = user.IsActive
            };


            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            User user = context.Users.First(u => u.Id == id);

            EditUserViewModel model = new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                EGN = user.EGN,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DismissDate = user.DismissDate,
                HireDate = user.HireDate,
                IsActive = user.IsActive
            };


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            User sameUserName = context.Users.FirstOrDefault(u => u.UserName == model.UserName);
            if (sameUserName != null && sameUserName.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "There is an user with this username.");
                //return Page();
            }

            User sameEGN = context.Users.FirstOrDefault(u => u.EGN == model.EGN);
            if (sameEGN != null && sameEGN.Id != model.Id)
            {
                ModelState.AddModelError("EGN", "There is an user with this EGN.");
                //return Page();
            }

            User samePhoneNumber = context.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
            if (samePhoneNumber != null && samePhoneNumber.Id != model.Id)
            {
                ModelState.AddModelError("PhoneNumber", "There is an user with this phone number.");
                //return Page();
            }

            if (context.Users.FirstOrDefault(u => u.Email == model.Email) != null)
            {
                ModelState.AddModelError("Email", "There is an user with this email.");
                //return Page();
            }



            if (ModelState.IsValid)
            {
                User user = context.Users.First(u => u.Id == model.Id);

                user.Id = model.Id;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.EGN = model.EGN;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.DismissDate = model.DismissDate;
                user.HireDate = model.HireDate;
                //user.IsActive = model.IsActive;

                context.Update(user);
                context.SaveChanges();

                return Redirect("~/User/Details/" + model.Id);
                //return Redirect("~/");
            }

            return View(model);
        }

        public IActionResult Hire(string id)
        {
            User user = context.Users.First(u => u.Id == id);

            user.HireDate = DateTime.Now;
            user.IsActive = true;

            context.Update(user);
            context.SaveChanges();

            return Redirect("~/User/Details/" + user.Id);
            //return Redirect("~/User/Employees");
        }

        public IActionResult Dismiss(string id)
        {
            User user = context.Users.First(u => u.Id == id);

            user.DismissDate = DateTime.Now;
            user.IsActive = false;

            context.Update(user);
            context.SaveChanges();

            return Redirect("~/User/Details/" + user.Id);
            //return Redirect("~/User/Employees");
        }

        public IActionResult Reservations(string id)
        {
            User user = context.Users.Find(id);

            UserViewModel userViewModel = new UserViewModel(user.Id, user.UserName, user.Email, user.FirstName,
                user.MiddleName, user.LastName, user.IsActive);

            List<ReservationViewModel> reservations = context.Reservations.Where(r => r.Creator.Id == id)
                .Select(r => new ReservationViewModel(r.Id, r.Room.Number,r.Room.Type, r.CheckInDate, 
                r.CheckOutDate, r.Price))
                .ToList();

            UserReservationsViewModel model = new UserReservationsViewModel(userViewModel, reservations);

            return View(model);
        }
    }
}