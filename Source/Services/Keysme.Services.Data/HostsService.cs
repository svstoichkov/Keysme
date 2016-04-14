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
                host.Amenities = new Amenities();
                host.UserId = userId;
                this.hosts.Add(host);
                this.hosts.SaveChanges();
            }

            return host;
        }

        public void CreateMainInformation(string userId, Host host)
        {
            var existingHost = this.GetWorkInProgressOrCreateNew(userId);
            
            existingHost.HostName = host.HostName;
            existingHost.Title = host.Title;
            existingHost.Description = host.Description;
            existingHost.Type = host.Type;
            existingHost.RoomType = host.RoomType;
            existingHost.RoomsCount = host.RoomsCount;
            existingHost.MaxGuests = host.MaxGuests;
            existingHost.BedsCount = host.BedsCount;
            existingHost.BathsCount = host.BathsCount;
            existingHost.Price = host.Price;
            existingHost.CurrencyId = host.CurrencyId;
            existingHost.IsInstantBook = host.IsInstantBook;
            existingHost.CancellationPolicy = host.CancellationPolicy;
            existingHost.CheckInAfter = host.CheckInAfter;
            existingHost.CheckOutBefore = host.CheckOutBefore;
            existingHost.WiFiName = host.WiFiName;
            existingHost.WiFiPassword = host.WiFiPassword;
            existingHost.HouseManual = host.HouseManual;
            existingHost.MainPhone = host.MainPhone;
            existingHost.ReservationPhone = host.ReservationPhone;
            existingHost.SmokingAllowed = host.SmokingAllowed;

            this.hosts.SaveChanges();
        }

        public void CreateLocation(string userId, Host host)
        {
            var existingHost = this.GetWorkInProgressOrCreateNew(userId);
            
            existingHost.Country = host.Country;
            existingHost.City = host.City;
            existingHost.State = host.State;
            existingHost.Address = host.Address;
            existingHost.Apartment = host.Apartment;
            existingHost.PostalCode = host.PostalCode;
            existingHost.Latitude = host.Latitude;
            existingHost.Longitude = host.Longitude;

            this.hosts.SaveChanges();
        }

        public void CreateAmenities(string userId, Amenities amenities)
        {
            var existingHost = this.GetWorkInProgressOrCreateNew(userId);

            existingHost.Amenities.AirConditioned = amenities.AirConditioned;
            existingHost.Amenities.BarOrLounge = amenities.BarOrLounge;
            existingHost.Amenities.ConciergeService = amenities.ConciergeService;
            existingHost.Amenities.ComplimentaryBreakfast = amenities.ComplimentaryBreakfast;
            existingHost.Amenities.ContinentalBreakfast = amenities.ContinentalBreakfast;
            existingHost.Amenities.Essentials = amenities.Essentials;
            existingHost.Amenities.Shampoo = amenities.Shampoo;
            existingHost.Amenities.Tv = amenities.Tv;
            existingHost.Amenities.Heating = amenities.Heating;
            existingHost.Amenities.Kitchen = amenities.Kitchen;
            existingHost.Amenities.Internet = amenities.Internet;
            existingHost.Amenities.Wifi = amenities.Wifi;
            existingHost.Amenities.HotTub = amenities.HotTub;
            existingHost.Amenities.Washer = amenities.Washer;
            existingHost.Amenities.Pool = amenities.Pool;
            existingHost.Amenities.Dryer = amenities.Dryer;
            existingHost.Amenities.ParkingFree = amenities.ParkingFree;
            existingHost.Amenities.FitnessCenter = amenities.FitnessCenter;
            existingHost.Amenities.Elevator = amenities.Elevator;
            existingHost.Amenities.SmokeDetector = amenities.SmokeDetector;
            existingHost.Amenities.CarbonMonoxideDetector = amenities.CarbonMonoxideDetector;
            existingHost.Amenities.FirstAidKit = amenities.FirstAidKit;
            existingHost.Amenities.SafetyCard = amenities.SafetyCard;
            existingHost.Amenities.FireExtinguisher = amenities.FireExtinguisher;
            existingHost.Amenities.AllTimeCheckin = amenities.AllTimeCheckin;
            existingHost.Amenities.Hangers = amenities.Hangers;
            existingHost.Amenities.HairDryer = amenities.HairDryer;
            existingHost.Amenities.Iron = amenities.Iron;
            existingHost.Amenities.DeskOrWorkspace = amenities.DeskOrWorkspace;
            existingHost.Amenities.FamilyFriendly = amenities.FamilyFriendly;
            existingHost.Amenities.SmokingAllowed = amenities.SmokingAllowed;
            existingHost.Amenities.PetsAllowed = amenities.PetsAllowed;
            existingHost.Amenities.WheelchairAccessible = amenities.WheelchairAccessible;
            existingHost.Amenities.HasPets = amenities.HasPets;
            existingHost.Amenities.AirportShuttleFree = amenities.AirportShuttleFree;

            this.hosts.SaveChanges();
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

            existingHost.HostName = host.HostName;
            existingHost.Title = host.Title;
            existingHost.Description = host.Description;
            existingHost.Type = host.Type;
            existingHost.RoomType = host.RoomType;
            existingHost.RoomsCount = host.RoomsCount;
            existingHost.MaxGuests = host.MaxGuests;
            existingHost.BedsCount = host.BedsCount;
            existingHost.BathsCount = host.BathsCount;
            existingHost.Price = host.Price;
            existingHost.CurrencyId = host.CurrencyId;
            existingHost.IsInstantBook = host.IsInstantBook;
            existingHost.CancellationPolicy = host.CancellationPolicy;
            existingHost.CheckInAfter = host.CheckInAfter;
            existingHost.CheckOutBefore = host.CheckOutBefore;
            existingHost.WiFiName = host.WiFiName;
            existingHost.WiFiPassword = host.WiFiPassword;
            existingHost.HouseManual = host.HouseManual;
            existingHost.MainPhone = host.MainPhone;
            existingHost.ReservationPhone = host.ReservationPhone;
            existingHost.SmokingAllowed = host.SmokingAllowed;

            existingHost.Country = host.Country;
            existingHost.City = host.City;
            existingHost.State = host.State;
            existingHost.Address = host.Address;
            existingHost.Apartment = host.Apartment;
            existingHost.PostalCode = host.PostalCode;
            existingHost.Latitude = host.Latitude;
            existingHost.Longitude = host.Longitude;
            
            existingHost.Comment = host.Comment;

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
