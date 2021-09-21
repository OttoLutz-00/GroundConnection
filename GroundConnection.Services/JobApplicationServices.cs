using GroundConnection.Data;
using GroundConnection.Models.JobApplication;
using GroundConnection.Models.JobModels;
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
                if(job is null || job.IsActive == false||job.OwnerId==_UserId)   // check if the job is still active
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
        // Approve a Job
        public bool ApproveJob(JobApproval model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var jobs = ctx.
                                JobApplications.SingleOrDefault(e => e.Id == model.JobApplicationId && e.Job.OwnerId == _UserId);

                if (jobs is null && jobs.JobStatus != StatusOfJob.Pending) return false;
                jobs.JobStatus = model.JobStatus;
                jobs.Job.IsActive = false;
                ctx.SaveChanges();

                var jobApplication = ctx.
                               JobApplications.Where(e => e.Job.Id == model.JobId && e.Job.OwnerId == _UserId);
                foreach(var job in jobApplication)
                {
                    if (job.JobStatus != StatusOfJob.Approved)
                    {
                        job.JobStatus = StatusOfJob.Declined;
                    }
                }



                return ctx.SaveChanges() >= 1;
            }
        }

        // Decline a Job
        public bool DeclineJob(JobApproval model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var jobs = ctx.
                               JobApplications.SingleOrDefault(e => e.Id == model.JobApplicationId && e.Job.OwnerId == _UserId);
                if (jobs is null || jobs.JobStatus != StatusOfJob.Pending) return false;
                jobs.JobStatus = model.JobStatus;
                return ctx.SaveChanges() == 1;

            }
        }
        // Get all Job application By Job Id
        public IEnumerable<JobApplicationListItem> GetJobApplicationsByJobId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var jobApplication = ctx.
                                        JobApplications.Where(e => e.JobId == id && e.Job.OwnerId == _UserId)
                                        .Select(e => new JobApplicationListItem
                                        {
                                            Id = e.Id,
                                            UserName = e.User.Name,
                                            AcceptedDate = e.DateAccepted,
                                            Skills = e.User.Skills
                                        }).ToList();
                return jobApplication;
            }
        }

        // Delete Job Application by user nthat applied for the Job
        public bool DeleteJobApplication(int id)
        {
            using(var ctx=new ApplicationDbContext())
            {
                var jobApplication = ctx
                                            .JobApplications
                                            .SingleOrDefault(e => e.Id == id && e.OwnerId == _UserId);
                if (jobApplication is null) return false;
                ctx.JobApplications.Remove(jobApplication);
                return ctx.SaveChanges() == 1;
            }
        }

        public JobApplicationDetail GetJobApplicationByJobApplicationId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var jobApplication = ctx
                                            .JobApplications
                                            .SingleOrDefault(e => e.Id == id && e.OwnerId == _UserId);
                if (jobApplication is null) return null;
                return new JobApplicationDetail()
                {
                    JobApplucationId = jobApplication.Id,
                    CustomerName = jobApplication.Job.User.Name,
                    DateCreated = jobApplication.Job.CreatedUTC,
                    Location = jobApplication.Job.Location
                };
            }
        }
        public bool UploadImageForCompletion(ProofOfCompletion model, int id)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var jobApplication = ctx
                                            .JobApplications
                                            .SingleOrDefault(e => e.Id == id && e.OwnerId == _UserId);
                if (jobApplication.JobStatus != StatusOfJob.Approved)
                {
                    return false;
                }
                if (jobApplication.ImageUrl != null) return false;
                jobApplication.ImageUrl = model.ImageUrl;
                return ctx.SaveChanges() == 1;

            }
        }

        public bool EditImage(ProofOfCompletion model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var jobApplication = ctx
                                            .JobApplications
                                            .SingleOrDefault(e => e.Id == id && e.OwnerId == _UserId);
                if (jobApplication.JobStatus != StatusOfJob.Approved)
                {
                    return false;
                }
                if (jobApplication.ImageUrl == null) return false;
                jobApplication.ImageUrl = model.ImageUrl;
                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteImage(int jobApplicationId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var jobApplication = ctx
                                            .JobApplications
                                            .SingleOrDefault(e => e.Id == jobApplicationId && e.OwnerId == _UserId);
                if (jobApplication.JobStatus != StatusOfJob.Approved)
                {
                    return false;
                }
                if (jobApplication.ImageUrl == null) return false;
                jobApplication.ImageUrl = null;
                return ctx.SaveChanges() == 1;

            }
        }





    }
}
