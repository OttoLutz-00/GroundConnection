using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobModels
{
    public class JobListItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
        public string JobDescription { get; set; }
        public DateTimeOffset ExpectedCompletionDate { get; set; }
        public bool IsActive { get; set; }
        public string Location { get; set; }
    }
}
