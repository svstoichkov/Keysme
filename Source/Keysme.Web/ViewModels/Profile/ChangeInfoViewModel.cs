namespace Keysme.Web.ViewModels.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Automapper;

    using Data.Models;

    using Global;

    public class ChangeInfoViewModel : IMapTo<User>, IMapFrom<User>
    {
        [Required]
        [MaxLength(ValidationConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserLastNameMaxLength)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        [MaxLength(ValidationConstants.UserAboutMaxLength)]
        public string About { get; set; }

        [MaxLength(ValidationConstants.UserLocationMaxLength)]
        public string Location { get; set; }

        [MaxLength(ValidationConstants.UserSchoolMaxLength)]
        public string School { get; set; }

        [MaxLength(ValidationConstants.UserWorkMaxLength)]
        public string Work { get; set; }

        [MaxLength(ValidationConstants.UserWorkMaxLength)]
        public string Languages { get; set; }

        [MaxLength(ValidationConstants.UserCommentMaxLength)]
        public string Comment { get; set; }

        public string PhoneNumber { get; set; }
    }
}