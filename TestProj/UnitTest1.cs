using NUnit.Framework;
using System.Linq;
using Movies;
using Movies.Entities;
using Microsoft.EntityFrameworkCore;

namespace MoviesTest
{
    public class Tests
    {
        [Test]
        public void AddUser_AddsSuccessfully()
        {
            using (AppDbContext db = new AppDbContext())
            {
                User user = new User
                {
                    Username = "testuser",
                    Password = "12345",
                    Email = "test@example.com"
                };

                db.Users.Add(user);
                db.SaveChanges();

                var addedUser = db.Users.FirstOrDefault(u => u.Username == "testuser");

                Assert.IsNotNull(addedUser);
                Assert.AreEqual("test@example.com", addedUser.Email);
            }
        }

        [Test]
        public void AddMovie_AssociatesWithUser()
        {
            using (AppDbContext db = new AppDbContext())
            {
                User user = new User
                {
                    Username = "movietester",
                    Password = "pass",
                    Email = "movie@test.com"
                };

                db.Users.Add(user);
                db.SaveChanges();

                Title movie = new Title
                {
                    Name = "Interstellar",
                    Year = 2014,
                    Description = "Space adventure",
                    UserId = user.Id
                };

                db.Titles.Add(movie);
                db.SaveChanges();

                var addedMovie = db.Titles.Include(t => t.User).FirstOrDefault(t => t.Name == "Interstellar");

                Assert.IsNotNull(addedMovie);
                Assert.IsNotNull(addedMovie.User);
                Assert.AreEqual("movietester", addedMovie.User.Username);
            }
        }
    }
}
