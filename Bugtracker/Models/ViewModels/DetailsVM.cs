using System.Collections.Generic;

namespace Bugtracker.Models.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Project = new Project();
        }
        public Project Project { get; set; }
        public IEnumerable<TicketModel> Tickets { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
