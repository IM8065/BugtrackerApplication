using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bugtracker.Models
{
    public class TicketModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public int SeverityId { get; set; }
        [ForeignKey("SeverityId")]
        public virtual Severity Severity { get; set; }
        [Display(Name = "Ticket Status")]
        public int TicketStatusId { get; set; }
        [ForeignKey("TicketStatusId")]
        public virtual Status TicketStatus { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
