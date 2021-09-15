using GroundConnection.Data;
using GroundConnection.Models.JobApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Services
{
    public class JobApplicationServices
    {
        private readonly Guid _UserId;
        public JobApplicationServices(Guid userId)
        {
            _UserId = userId;
        }

        public bool ApplyForJob(JobApplicationCreate model)
        {
            using(var ctx= new ApplicationDbContext())
            {
                var job = ctx
                                .Jobs.SingleOrDefault(e => e.Id == model.JobId);
                if(job is null || job.IsActive == false)   // check if the job is still active
                {
                    return false;
                }
                //check if Applicatnt has applied before for the job
                var jobApplication = ctx.JobApplications.SingleOrDefault(e => e.OwnerId == _UserId&&e.JobId==model.JobId);

                if (jobApplication != null) return false;
                var apply = new JobApplication()
                {
                    JobId = model.JobId,
                    OwnerId = _UserId,
                    UserId = model.UserId,
                    DateAccepted = DateTimeOffset.Now
                };
                ctx.JobApplications.Add(apply);
                return ctx.SaveChanges() == 1;

               
            }
        }

       
        
    }
}
