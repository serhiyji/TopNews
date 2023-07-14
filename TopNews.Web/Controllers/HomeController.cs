﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TopNews.Web.Models;

namespace TopNews.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                default:
                    return View("Error");
            }
        }
    }
}