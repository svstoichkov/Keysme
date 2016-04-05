namespace Keysme.Services.Data
{
    using System;

    using Keysme.Data;
    using Keysme.Data.Models;
    using System.Drawing.Imaging;
    using System.IO;

    using Contracts;

    using Global;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public void AddProfileImage(string userId, System.Drawing.Image image)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var imagePath = Path.Combine(basePath, Path.Combine(GlobalConstants.UserProfileImageFolder, userId + ".jpg"));
            image.Save(imagePath, ImageFormat.Jpeg);

            var user = this.users.GetById(userId);
            user.ProfileImage = userId + ".jpg";
            this.users.SaveChanges();
        }

        public void Update(string userId, User user)
        {
            var existingUser = this.users.GetById(userId);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Gender = user.Gender;
            existingUser.BirthDate = user.BirthDate;
            existingUser.About = user.About;
            existingUser.LastName = user.Location;
            existingUser.School = user.School;
            existingUser.Work = user.Work;
            existingUser.Languages = user.Languages;
            existingUser.Comment = user.Comment;
            existingUser.PhoneNumber = user.PhoneNumber;

            this.users.SaveChanges();
        }
    }
}
