namespace Keysme.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using Contracts;

    using Global;

    using Keysme.Data;
    using Keysme.Data.Models;

    using Image = System.Drawing.Image;

    public class HostsService : IHostsService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Host> hosts;
        private readonly IRepository<Keysme.Data.Models.Image> images;

        public HostsService(IRepository<User> users, IRepository<Host> hosts, IRepository<Keysme.Data.Models.Image> images)
        {
            this.users = users;
            this.hosts = hosts;
            this.images = images;
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

        public void CreateAmenities(string userId, Host host)
        {
            var existingHost = this.GetWorkInProgressOrCreateNew(userId);

            existingHost.AirConditioned = host.AirConditioned;
            existingHost.BarOrLounge = host.BarOrLounge;
            existingHost.ConciergeService = host.ConciergeService;
            existingHost.ComplimentaryBreakfast = host.ComplimentaryBreakfast;
            existingHost.ContinentalBreakfast = host.ContinentalBreakfast;
            existingHost.Essentials = host.Essentials;
            existingHost.Shampoo = host.Shampoo;
            existingHost.Tv = host.Tv;
            existingHost.Heating = host.Heating;
            existingHost.Kitchen = host.Kitchen;
            existingHost.Internet = host.Internet;
            existingHost.Wifi = host.Wifi;
            existingHost.HotTub = host.HotTub;
            existingHost.Washer = host.Washer;
            existingHost.Pool = host.Pool;
            existingHost.Dryer = host.Dryer;
            existingHost.ParkingFree = host.ParkingFree;
            existingHost.FitnessCenter = host.FitnessCenter;
            existingHost.Elevator = host.Elevator;
            existingHost.SmokeDetector = host.SmokeDetector;
            existingHost.CarbonMonoxideDetector = host.CarbonMonoxideDetector;
            existingHost.FirstAidKit = host.FirstAidKit;
            existingHost.SafetyCard = host.SafetyCard;
            existingHost.FireExtinguisher = host.FireExtinguisher;
            existingHost.AllTimeCheckin = host.AllTimeCheckin;
            existingHost.Hangers = host.Hangers;
            existingHost.HairDryer = host.HairDryer;
            existingHost.Iron = host.Iron;
            existingHost.DeskOrWorkspace = host.DeskOrWorkspace;
            existingHost.FamilyFriendly = host.FamilyFriendly;
            existingHost.SmokingAllowed = host.SmokingAllowed;
            existingHost.PetsAllowed = host.PetsAllowed;
            existingHost.WheelchairAccessible = host.WheelchairAccessible;
            existingHost.HasPets = host.HasPets;
            existingHost.AirportShuttleFree = host.AirportShuttleFree;

            this.hosts.SaveChanges();
        }

        public void CreateImages(string userId, IEnumerable<Image> images)
        {
            var existingHost = this.GetWorkInProgressOrCreateNew(userId);

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var imagePath = Path.Combine(basePath, Path.Combine(GlobalConstants.HostImagesFolder, existingHost.Id.ToString()));
            if (Directory.Exists(imagePath))
            {
                Directory.Delete(imagePath, true);
            }
            Directory.CreateDirectory(imagePath);

            foreach (var image in existingHost.Images.ToList())
            {
                this.images.Delete(image);
            }

            var resizedImages = images.Select(image => image.ResizeImage(1000, 562)).ToList();
            for (int i = 0; i < resizedImages.Count; i++)
            {
                var name = existingHost.Id + $"_{i + 1}.jpg";
                resizedImages[i].Save(Path.Combine(imagePath, name));

                existingHost.Images.Add(new Keysme.Data.Models.Image { Filename = name });
            }

            this.hosts.SaveChanges();
        }

        public int CreatePublish(string userId)
        {
            var existingHost = this.GetWorkInProgressOrCreateNew(userId);
            existingHost.IsComplete = true;
            this.hosts.SaveChanges();

            return existingHost.Id;
        }

        public void Create(string userId, Host host, Host amenities)
        {
            var user = this.users.GetById(userId);
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
        
        public void Update(string userId, int hostId, Host host, Host amenities)
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
