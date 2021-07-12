using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Spotters.Data.Models
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid InternalId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public String CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser CreatedByInfo { get; set; }
        public String UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public ApplicationUser UpdatedByInfo { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
