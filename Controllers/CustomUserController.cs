/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Explorer.Data;
using Movie_Explorer.Models;

namespace Movie_Explorer.Controllers
{
    public class CustomUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomUserController(ApplicationDbContext context)
        {
            _context = context;
        }
    }

    public async Task<IActionResult> Index(string userEmail)
    {
        var users = from u in _context.CustomUser select u;

        if (!string.IsNullOrEmpty(userEmail))
        {
            users = users.Where(e => e.Email!.Contains(userEmail));
        }

        return View(userEmail);
    }



}
*/