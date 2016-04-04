namespace Keysme.Web.Controllers.MVC
{
    using System.Web.Mvc;

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
            this.ViewBag.Title = "Home Page";

            return this.View();
        }
    }
}
