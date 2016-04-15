namespace Keysme.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Keysme.Data.Models;

    using Image = System.Drawing.Image;

    public interface IHostsService
    {
        void Create(string getUserId, Host host, Amenities amenities);
        
        IQueryable<Host> GetOwn(string userId);
        
        void Update(string getUserId, int hostId, Host host, Amenities amenities);
        
        void Delete(string getUserId, int id);
        
        IQueryable<Host> GetAll();

        Host GetWorkInProgressOrCreateNew(string userId);

        void CreateMainInformation(string userId, Host host);

        void CreateLocation(string userId, Host host);

        void CreateAmenities(string userId, Amenities amenities);

        void CreateImages(string userId, IEnumerable<Image> images);

        void CreatePublish(string userId);
    }
}