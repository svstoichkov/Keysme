namespace Keysme.Services.Data
{
    using System.Linq;

    using Keysme.Data.Models;

    public interface IHostsService
    {
        void Create(string getUserId, Host host, Amenities amenities);

        IQueryable<Host> Read(string userId);

        void Update(string getUserId, int hostId, Host host, Amenities amenities);

        void Delete(string getUserId, int id);
    }
}