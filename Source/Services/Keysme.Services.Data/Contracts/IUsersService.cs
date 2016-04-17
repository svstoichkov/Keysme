namespace Keysme.Services.Data.Contracts
{
    using System.Linq;

    using Keysme.Data.Models;

    using Image = System.Drawing.Image;

    public interface IUsersService
    {
        void AddProfileImage(string userId, Image image);

        void Update(string userId, User user);

        void RequestVerification(string userId, VerificationType type, CountryCode countryCode, Image frontImage, Image backImage);

        User GetUser(string userId);

        IQueryable<User> GetAll();

        void Verify(string userId);
    }
}