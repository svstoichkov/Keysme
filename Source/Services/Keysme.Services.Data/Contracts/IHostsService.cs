namespace Keysme.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Keysme.Data.Models;

    using Image = System.Drawing.Image;

    public interface IHostsService
    {
        IQueryable<Host> GetOwn(string userId);
        
        void Delete(string userId, int id);
        
        IQueryable<Host> GetAll();

        Host GetWorkInProgressOrCreateNew(string userId);

        void CreateMainInformation(string userId, Host host);

        void CreateLocation(string userId, Host host);

        void CreateAmenities(string userId, Host host);

        void CreateImages(string userId, IEnumerable<Image> images);

        int CreatePublish(string userId);

        void AdminDelete(string userId, int hostId);

        void AdminApprove(string userId, int hostId);
    }
}