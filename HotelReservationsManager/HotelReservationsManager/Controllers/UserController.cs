using System;
using System.Collections.Generic;
using System.Linq;
using HotelReservationsManager.Data;
using HotelReservationsManager.Data.Models;
using HotelReservationsManager.Models;
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

        public IActionResult Employees()
        {
            List<UserViewModel> employees = 
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId == 
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
                                 HireDate = u.HireDate,
                                 IsActive = u.IsActive,
                                 DismissDate = u.DismissDate
                             })
                             .ToList();
           
            return View(employees);
        }

        public IActionResult EmployeesByUsername(UsersByUsernameViewModel model)
        {
            model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.UserName == model.UserName)
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
                                 HireDate = u.HireDate,
                                 IsActive = u.IsActive,
                                 DismissDate = u.DismissDate
                             })
                             .ToList();

            return View(model);
        }

        public IActionResult EmployeesByFirstName(UsersByFirstNameViewModel model)
        {
            model.Users =
                context.Users.Where(u => context.UserRoles.First(ur => ur.UserId == u.Id).RoleId ==
                                            context.Roles.First(r => r.Name == "Employee").Id)
                             .Where(u => u.FirstName == model.FirstName)
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
                                 HireDate = u.HireDate,
                                 IsActive = u.IsActive,
                                 DismissDate = u.DismissDate
                             })
                             .ToList();

            return View(model);
        }

        public IActionResult EmployeesByMiddleName(string middleName)
        {
            throw new NotImplementedException();
        }

        public IActionResult EmployeesByLAstName(string lastName)
        {
            throw new NotImplementedException();
        }

        public IActionResult EmployeesByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IActionResult SearchEmployees(SearchEmployeesViewModel model)
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
                                 HireDate = u.HireDate,
                                 IsActive = u.IsActive,
                                 DismissDate = u.DismissDate
                             })
                             .ToList();

                return View(model);
            }

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
                                 HireDate = u.HireDate,
                                 IsActive = u.IsActive,
                                 DismissDate = u.DismissDate
                             })
                             .ToList();

            return View(model);
        }
    }
}