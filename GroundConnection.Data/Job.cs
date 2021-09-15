using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Data
{
    public class Job
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public Guid OwnerId { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
        [Required]
        public string JobDescription { get; set; }
        public DateTimeOffset ExpectedCompletionDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string Location { get; set; }
        public virtual List<JobApplication> JobApplications { get; set; }

    }
}
