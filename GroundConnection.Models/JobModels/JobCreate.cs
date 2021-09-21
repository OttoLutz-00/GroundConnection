using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobModels
{
    public class JobCreate
    {
        public int UserId { get; set; }
        public string JobDescription { get; set; }
        public DateTimeOffset ExpectedCompletionDate { get; set; }
        public string Location { get; set; }

    }
}
