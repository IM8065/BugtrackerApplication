using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugtracker.Models.ViewModels
{
    public class TicketVM
    {
        public TicketModel TicketModel { get; set; }
        public IEnumerable<SelectListItem> SeverityList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> AvailableProjects { get; set; }
    }
}
