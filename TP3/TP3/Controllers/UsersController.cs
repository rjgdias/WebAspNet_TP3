using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models;

namespace TP3.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var users = from u in _context.Users
                       select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString));
            }
            return View(users);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else return View(user);
        }

        [HttpGet]
        public IActionResult AttUser(int? Id)
        {
            if (Id != null)
            {
                User user = _context.Users.Find(Id);
                return View(user);
            }
            else return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AttUser(int? Id, User user)
        {
            if (Id != null)
            {
                if (ModelState.IsValid)
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else return View(user);
            }
            else return NotFound();
        }


        [HttpGet]
        public IActionResult DelUser(int? Id)
        {
            if (Id != null)
            {
                User user = _context.Users.Find(Id);
                return View(user);
            }
            else return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DelUser(int? Id, User user)
        {
            if (Id != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var user = _context.Users.Find(Id);
            return View(user);
        }
    }
}