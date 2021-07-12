using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Service.ViewModels
{
    public class BaseViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public Guid InternalId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public String CreatedBy { get; set; }
        public String UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
