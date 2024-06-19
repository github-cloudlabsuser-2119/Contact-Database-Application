using CRUD_application_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        private static List<User> _users = new List<User>();

        // GET: User
        public ActionResult Index(string searchString)
        {
            var users = from u in _users
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                // Perform a case-insensitive search on both Name and Email
                users = users.Where(s => s.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                         || s.Email.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                         || s.Phone.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Simple ID assignment for demonstration purposes
                user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
                _users.Add(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = _users.FirstOrDefault(u => u.Id == id);
                if (userToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Update properties
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                // Add other properties as needed

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
