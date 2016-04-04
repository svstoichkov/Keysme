namespace Keysme.Web.Controllers.MVC
{
    using System.Web.Mvc;

    using Automapper;

    using AutoMapper;

    public abstract class BaseController : Controller
    {
        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();
    }
}