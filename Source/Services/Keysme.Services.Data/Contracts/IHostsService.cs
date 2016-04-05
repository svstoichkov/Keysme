namespace Keysme.Services.Data
{
    using Keysme.Data.Models;

    public interface IHostsService
    {
        void Create(string getUserId, Host host, Amenities amenities);
    }
}