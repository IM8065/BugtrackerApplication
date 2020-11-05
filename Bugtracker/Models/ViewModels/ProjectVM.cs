using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugtracker.Models.ViewModels
{
    public class ProjectVM
    {
        public Project Project { get; set; }
        public IEnumerable<SelectListItem> UserSelectList { get; set; }
        public IEnumerable<TicketModel> TicketList { get; set; }
    }
}
