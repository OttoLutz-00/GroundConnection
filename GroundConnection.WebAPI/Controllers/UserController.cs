using GroundConnection.Models;
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
    public class UserController : ApiController
    {
        private ServiceUser CreateUserService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var userService = new ServiceUser(userId);
            return userService;
        }
        public IHttpActionResult Post(UserCreate User)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newUser = CreateUserService();

            if (!newUser.CreateUser(User))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Get()
        {
            ServiceUser userService = CreateUserService();
            var user = userService.GetUsers();
            return Ok(user);
        }

        public IHttpActionResult Get(int id)
        {
            ServiceUser serviceUser = CreateUserService();
            var user = serviceUser.GetUserById(id);
            return Ok();
        }

        public IHttpActionResult Put(UpdateUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var updatedUser = CreateUserService();

            if (!updatedUser.UpdateUser(user))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var user = CreateUserService();

            if (!user.DeleteUser(id))
                return InternalServerError();

            return Ok();
        }
    }
}
