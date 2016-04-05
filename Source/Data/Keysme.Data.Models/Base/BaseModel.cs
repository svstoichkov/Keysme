namespace Keysme.Data.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseModel : IAuditInfo, IDeletableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
