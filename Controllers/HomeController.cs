﻿using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult StartPage()
        {
            return View();
        }
    }
}
