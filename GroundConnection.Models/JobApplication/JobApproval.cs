using GroundConnection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobApplication
{
    public class JobApproval
    {
        public int JobApplicationId { get; set; }
        public StatusOfJob JobStatus { get; set; }
    }
}
