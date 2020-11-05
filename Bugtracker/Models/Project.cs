using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Bugtracker.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Short Description")]
        public string ShortDesc { get; set; }
        public ICollection<TicketModel> AssignedTickets { get; set; }
    }
}
