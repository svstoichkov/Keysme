using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keysme.Web.Controllers
{
    using Data;
    using Data.Models;

    public class HomeController : Controller
    {
        private readonly IRepository<User> usersRepository;

        public HomeController(IRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
