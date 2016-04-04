namespace Keysme.Web.Controllers.WebApi
{
    using System.Web.Http;

    using Automapper;

    using AutoMapper;

    public abstract class BaseController : ApiController
    {
        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();
    }
}