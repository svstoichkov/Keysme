namespace Keysme.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class User : IdentityUser
    {
        private ICollection<Host> hosts;

        public User()
        {
            this.Hosts = new HashSet<Host>();
        }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(1000)]
        public string About { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Location { get; set; }

        [Required]
        [MaxLength(1000)]
        public string School { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Work { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Languages { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        [MaxLength(100)]
        public string FrontPicture { get; set; }

        [Required]
        [MaxLength(100)]
        public string BackPicture { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryCodeIssued { get; set; }

        public ICollection<Host> Hosts
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
    }
}