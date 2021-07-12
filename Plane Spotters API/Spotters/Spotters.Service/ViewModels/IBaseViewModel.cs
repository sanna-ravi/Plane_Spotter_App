using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Service.ViewModels
{
    public interface IBaseViewModel
    {
        int Id { get; set; }
        Guid InternalId { get; set; }
        DateTime? CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
        String CreatedBy { get; set; }
        String UpdatedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
