namespace Keysme.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Base;

    using Global;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class User : IdentityUser, IAuditInfo
    {
        private ICollection<Host> hosts;

        public User()
        {
            this.Hosts = new HashSet<Host>();
            this.ProfileImage = "user.jpg";
        }

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

        public virtual Verification Verification { get; set; }

        [MaxLength(300)]
        public string ProfileImage { get; set; }

        public virtual ICollection<Host> Hosts
        {
            get
            {
                return this.hosts;
            }
            set
            {
                this.hosts = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}