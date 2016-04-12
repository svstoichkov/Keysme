namespace Keysme.Services.Data.Contracts
{
    using Keysme.Data.Models;

    using Image = System.Drawing.Image;

    public interface IUsersService
    {
        void AddProfileImage(string userId, Image image);

        void Update(string userId, User user);

        void Verify(string userId, string type, string countryCode, Image frontImage, Image backImage);

        User GetUser(string userId);
    }
}