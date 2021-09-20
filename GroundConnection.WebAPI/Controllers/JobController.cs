using GroundConnection.Models.JobModels;
using GroundConnection.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroundConnection.WebAPI.Controllers
{
    [Authorize]
    public class JobController : ApiController
    {
        private JobService2 CreateJobService()
        {
            // get user id
            var userId = Guid.Parse(User.Identity.GetUserId());

            //use user id to make and return service
            var jobService = new JobService2(userId);
            return jobService;
        }

        // POST
        [HttpPost]
        public IHttpActionResult Post(JobCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateJobService();

            if (service.CreateJob(model))
            {
                return Ok("Job created successfully.");
            }
            return InternalServerError();
            
        }

        // GET
        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = CreateJobService();

            var jobs = service.GetJobs();

            if (jobs is null)
            {
                return Ok("No jobs were found.");
            }

            return Ok(jobs);
        }

        // GET BY ID
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var service = CreateJobService();

            var job = service.GetJobById(id);
            if (job is null)
            {
                return Ok("No job was found.");
            }

            return Ok(job);

        }

        // UPDATE
        [HttpPut]
        public IHttpActionResult Put(JobEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateJobService();

            if (!service.UpdateJob(model))
            {
                return BadRequest("Error Bad Request, make sure there are no existing applications for this job.");
            }
            return Ok("Job updated successfully.");
        }

        // UPDATE (soft delete)... IsActive = false
        [HttpPut]
        public IHttpActionResult UpdateJobIsActive(int id)
        {

            var service = CreateJobService();

            if (!service.UpdateJobActive(id))
            {
                return InternalServerError();
            }
            return Ok("Job IsActive status updated.");
        }
    }
}
