namespace Keysme.Services.Data
{
    using System.Linq;

    using Contracts;

    using Keysme.Data;
    using Keysme.Data.Models;

    public class HostsService : IHostsService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Host> hosts;
        private readonly IRepository<Amenities> amenitiesRepository;

        public HostsService(IRepository<User> users, IRepository<Host> hosts, IRepository<Amenities> amenitiesRepository)
        {
            this.users = users;
            this.hosts = hosts;
            this.amenitiesRepository = amenitiesRepository;
        }

        public Host GetWorkInProgressOrCreateNew(string userId)
        {
            var host = this.hosts.All().FirstOrDefault(x => x.UserId == userId && x.IsComplete == false);
            if (host == null)
            {
                host = new Host();
                host.UserId = userId;
                this.hosts.Add(host);
                this.hosts.SaveChanges();
            }

            return host;
        }

        public void Create(string userId, Host host, Amenities amenities)
        {
            var user = this.users.GetById(userId);
            host.Amenities = amenities;
            user.Hosts.Add(host);
            this.users.SaveChanges();
        }

        public IQueryable<Host> GetOwn(string userId)
        {
            return this.hosts.All().Where(x => x.UserId == userId && !x.IsDeleted);
        }

        public IQueryable<Host> GetAll()
        {
            return this.hosts.All().Where(x => !x.IsDeleted);
        }

        public void Update(string userId, int hostId, Host host, Amenities amenities)
        {
            var existingHost = this.hosts.All().First(x => x.Id == hostId && x.UserId == userId);

            existingHost.Type = host.Type;
            existingHost.RoomType = host.RoomType;
            existingHost.MaxGuests = host.MaxGuests;
            existingHost.RoomsCount = host.RoomsCount;
            existingHost.BedsCount = host.BedsCount;
            existingHost.BathsCount = host.BathsCount;
            existingHost.City = host.City;
            existingHost.Description = host.Description;
            existingHost.Header = host.Header;
            existingHost.Price = host.Price;
            existingHost.CurrencyId = host.CurrencyId;
            existingHost.IsInstantBook = host.IsInstantBook;
            existingHost.HostName = host.HostName;
            existingHost.SmokingAllowed = host.SmokingAllowed;
            existingHost.Address = host.Address;
            existingHost.State = host.State;
            existingHost.PostalCode = host.PostalCode;
            // TODO: !!!
            //existingHost.CountryCode = host.CountryCode;
            existingHost.Latitude = host.Latitude;
            existingHost.Longitude = host.Longitude;
            existingHost.LocationName = host.LocationName;
            existingHost.Comment = host.Comment;
            existingHost.CheckInAfter = host.CheckInAfter;
            existingHost.CheckOutBefore = host.CheckOutBefore;
            existingHost.CancellationPolicy = host.CancellationPolicy;
            existingHost.WiFiName = host.WiFiName;
            existingHost.WiFiPassword = host.WiFiPassword;
            existingHost.HouseManual = host.HouseManual;
            existingHost.MainPhone = host.MainPhone;
            existingHost.ReservationPhone = host.ReservationPhone;

            if (existingHost.Amenities != null)
            {
                this.amenitiesRepository.Delete(existingHost.Amenities);
            }
            existingHost.Amenities = amenities;

            this.hosts.SaveChanges();
        }

        public void Delete(string userId, int hostId)
        {
            var existingHost = this.hosts.All().First(x => x.Id == hostId && x.UserId == userId);
            existingHost.IsDeleted = true;
            this.hosts.SaveChanges();
        }
    }
}
