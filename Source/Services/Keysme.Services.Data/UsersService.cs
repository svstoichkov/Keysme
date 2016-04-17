namespace Keysme.Services.Data
{
    using System;

    using Keysme.Data;
    using Keysme.Data.Models;
    using System.Drawing.Imaging;
    using System.IO;

    using Contracts;

    using Global;

    using Image = System.Drawing.Image;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Verification> verifications;

        public UsersService(IRepository<User> users, IRepository<Verification> verifications)
        {
            this.users = users;
            this.verifications = verifications;
        }

        public void AddProfileImage(string userId, Image image)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var imagePath = Path.Combine(basePath, Path.Combine(GlobalConstants.UserProfileImageFolder, userId + ".jpg"));
            image = image.ResizeImageWithBorders(440, 440);
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
            existingUser.Location = user.Location;
            existingUser.School = user.School;
            existingUser.Work = user.Work;
            existingUser.Languages = user.Languages;
            existingUser.Comment = user.Comment;
            existingUser.PhoneNumber = user.PhoneNumber;

            this.users.SaveChanges();
        }

        public void Verify(string userId, VerificationType type, CountryCode countryCode, Image frontImage, Image backImage)
        {
            var verification = new Verification();
            verification.Type = type;
            verification.CountryCode = countryCode;
            verification.FrontPicture = userId + "_front.jpg";
            verification.BackPicture = userId + "_back.jpg";

            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var frontImagePath = Path.Combine(basePath, Path.Combine(GlobalConstants.UserVerificationImageFolder, verification.FrontPicture));
            var backImagePath = Path.Combine(basePath, Path.Combine(GlobalConstants.UserVerificationImageFolder, verification.BackPicture));

            var user = this.users.GetById(userId);
            if (user.Verification != null)
            {
                this.verifications.Delete(user.Verification);
            }

            user.Verification = verification;
            this.users.SaveChanges();
            frontImage.Save(frontImagePath, ImageFormat.Jpeg);
            backImage.Save(backImagePath, ImageFormat.Jpeg);
        }

        public User GetUser(string userId)
        {
            return this.users.GetById(userId);
        }
    }
}
