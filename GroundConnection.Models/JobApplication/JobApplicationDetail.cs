using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobApplication
{
   public class JobApplicationDetail
    {
        public int JobApplucationId { get; set; }
        [Display(Name ="Job Owner")]
        public string CustomerName { get; set; }
        [Display(Name = "Job Creation Date")]
        public DateTimeOffset DateCreated { get; set; }
        public string Location { get; set; }
        
    }
}
