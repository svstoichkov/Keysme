namespace Keysme.Web.Controllers.MVC
{
    using System.Web.Mvc;

    public class LandingController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}