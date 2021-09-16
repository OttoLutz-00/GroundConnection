using GroundConnection.Data;
using GroundConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Services
{
    public class ServiceUser
    {
        //Create / Post
        private readonly Guid _userId;
        public ServiceUser(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateUser(UserCreate model)
        {
            var userInfo =
                new User()
                {
                    Name = model.Name,
                    Location = model.Location,
                    OwnerId = _userId,
                    PhoneNumber = model.PhoneNumber,
                    EmailAddress = model.EmailAddress,
                    Skills = model.Skills
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Users.Add(userInfo);
                return ctx.SaveChanges() == 1;
            }

            // instead of creating a new variable (userInfo), storing the data there, then passing in the variable to create a new User, we can just add the new user without creating the variable, like below, saving lines of code and some memory


            //using (var ctx = new ApplicationDbContext())
            //{
            //    ctx.Users.Add(new User()
            //    {
            //        Name = model.Name,
            //        Location = model.Location,
            //        OwnerId = _userId,
            //        PhoneNumber = model.PhoneNumber,
            //        EmailAddress = model.EmailAddress,
            //        Skills = model.Skills
            //    });
            //    return ctx.SaveChanges() == 1;
            //}

        }

        //Read / Get
        public IEnumerable<UserListItem> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var allUsers =
                    ctx
                    .Users
                    .Where(u => u.OwnerId == _userId)
                    .Select(
                        u =>
                        new UserListItem
                        {
                            Name = u.Name,
                            Location = u.Location,
                            PhoneNumber = u.PhoneNumber,
                            EmailAddress = u.EmailAddress,
                            Skills = u.Skills
                        });
                return allUsers.ToList();
            }
        }

        public UserDetails GetUserById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user =
                    ctx
                    .Users
                    .Single(u => u.Id == id && u.OwnerId == _userId);
                return
                    new UserDetails
                    {
                        Name = user.Name,
                        Location = user.Location,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Skills = user.Skills
                    };
            }
        }

        //Update / Put

         public bool UpdateUser(UpdateUser model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var updateUser =
                    ctx
                    .Users
                    .Single(u => u.Id == model.Id && u.OwnerId == _userId);

                updateUser.Id = model.Id;
                updateUser.Name = model.Name;
                updateUser.Location = model.Location;
                updateUser.PhoneNumber = model.PhoneNumber;
                updateUser.EmailAddress = model.EmailAddress;
                updateUser.Skills = model.Skills;

                return ctx.SaveChanges() == 1;
            }
        }

        //Delete
        public bool DeleteUser(int userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user =
                    ctx
                    .Users
                    .Single(u => u.Id == userId && u.OwnerId == _userId);
                ctx.Users.Remove(user);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
