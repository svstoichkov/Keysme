namespace Keysme.Web.ViewModels.Host
{
    using Automapper;

    using Data.Models;

    public class AmenitiesViewModel : IMapTo<Amenities>
    {
        public bool AirConditioned { get; set; }

        public bool BarOrLounge { get; set; }

        public bool ConciergeService { get; set; }

        public bool ComplimentaryBreakfast { get; set; }

        public bool ContinentalBreakfast { get; set; }

        public bool Essentials { get; set; }

        public bool Shampoo { get; set; }

        public bool Tv { get; set; }

        public bool Heating { get; set; }

        public bool Kitchen { get; set; }

        public bool Internet { get; set; }

        public bool Wifi { get; set; }

        public bool HotTub { get; set; }

        public bool Washer { get; set; }

        public bool Pool { get; set; }

        public bool Dryer { get; set; }

        public bool ParkingFree { get; set; }

        public bool FitnessCenter { get; set; }

        public bool Elevator { get; set; }

        public bool SmokeDetector { get; set; }

        public bool CarbonMonoxideDetector { get; set; }

        public bool FirstAidKit { get; set; }

        public bool SafetyCard { get; set; }

        public bool FireExtinguisher { get; set; }

        public bool AllTimeCheckin { get; set; }

        public bool Hangers { get; set; }

        public bool HairDryer { get; set; }

        public bool Iron { get; set; }

        public bool DeskOrWorkspace { get; set; }

        public bool FamilyFriendly { get; set; }

        public bool SmokingAllowed { get; set; }

        public bool PetsAllowed { get; set; }

        public bool WheelchairAccessible { get; set; }

        public bool HasPets { get; set; }

        public bool AirportShuttleFree { get; set; }
    }
}