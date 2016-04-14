namespace Keysme.Services.Data.Contracts
{
    using System.Linq;

    using Keysme.Data.Models;

    public interface IHostsService
    {
        void Create(string getUserId, Host host, Amenities amenities);

        IQueryable<Host> GetOwn(string userId);

        void Update(string getUserId, int hostId, Host host, Amenities amenities);

        void Delete(string getUserId, int id);

        IQueryable<Host> GetAll();

        Host GetWorkInProgressOrCreateNew(string userId);

        void AddMainInformation(string userId, Host host);
    }
}