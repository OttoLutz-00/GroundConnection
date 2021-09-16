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
    public class JobService2
    {
        private readonly Guid _userId;

        public JobService2(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateJob(JobCreate model)
        {
            var entity = new Job()
            {
                UserId = model.UserId,
                OwnerId = _userId,
                CreatedUTC = DateTimeOffset.Now,
                JobDescription = model.JobDescription,
                ExpectedCompletionDate = model.ExpectedCompletionDate,
                IsActive = true,
                Location = model.Location
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Jobs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<JobListItem> GetJobs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx.Jobs.Where(e => e.OwnerId == _userId)
                .Select(e => new JobListItem()
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    CreatedUTC = e.CreatedUTC,
                    JobDescription = e.JobDescription,
                    ExpectedCompletionDate = e.ExpectedCompletionDate,
                    IsActive = e.IsActive,
                    Location = e.Location,
                });
                return query.ToArray();
            }
        }

        public JobDetail GetJobById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Jobs.Single(e => e.Id == id);

                var service = new JobApplicationServices(_userId);
                var jobApplications = service.GetJobApplicationsByJobId(id);
                
                return new JobDetail()
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    CreatedUTC = entity.CreatedUTC,
                    JobDescription = entity.JobDescription,
                    ExpectedCompletionDate = entity.ExpectedCompletionDate,
                    IsActive = entity.IsActive,
                    Location = entity.Location,
                    JobApplications = jobApplications
                };
            }
        }
    }
}
