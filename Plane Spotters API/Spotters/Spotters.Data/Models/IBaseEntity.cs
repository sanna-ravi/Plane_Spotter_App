using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Spotters.Data.Models
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        Guid InternalId { get; set; }
        DateTime? CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
        String CreatedBy { get; set; }
        String UpdatedBy { get; set; }
        [DefaultValue(false)]
        bool IsDeleted { get; set; }
    }
}
