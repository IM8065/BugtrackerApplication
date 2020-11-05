using System.Collections.Generic;

namespace Bugtracker.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}
