using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobApplication
{
   public  class JobApplicationListItem
    {
        [Display(Name ="Job Applicant")]
        public string UserName { get; set; }
        [Display(Name = "Applicant's Skills")]
        public string Skills { get; set; }
        [Display(Name = "Job Acceptance Date")]
        public DateTimeOffset? AcceptedDate { get; set; }
        public int Id { get; set; }


    }
}
