namespace Keysme.Services.Data.Contracts
{
    using System.Drawing;

    public interface IUsersService
    {
        void AddProfileImage(string userId, Image image);
    }
}