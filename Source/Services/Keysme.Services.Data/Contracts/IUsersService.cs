namespace Keysme.Services.Data.Contracts
{
    using Keysme.Data.Models;

    using Image = System.Drawing.Image;

    public interface IUsersService
    {
        void AddProfileImage(string userId, Image image);

        void Update(string userId, User user);
    }
}