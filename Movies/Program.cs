using Microsoft.EntityFrameworkCore;
using Movies.Entities;
using System.Text.RegularExpressions;

namespace Movies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            AppDbContext db = new AppDbContext();

            int choose;
            do
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                choose = int.Parse(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        RegUser(db);
                        break;
                    case 2:
                        LoginUser();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choose != 3);
        }

        private static void RegUser(AppDbContext db)
        {
            User user = new User();
            Console.WriteLine("Введіть ім'я користувача: ");
            user.Username = Console.ReadLine();
            Console.WriteLine("Введіть пошту (email) користувача: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Введіть пароль користувача: ");
            user.Password = Console.ReadLine();
            db.Users.Add(user);
            db.SaveChanges();
        }

        private static void LoginUser()
        {
            Console.WriteLine("Введіть пошут (email): ");
            string email = Console.ReadLine();
            Console.WriteLine("Введіть пароль: ");
            string password = Console.ReadLine();

            using (AppDbContext db = new AppDbContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    Console.WriteLine("Вхід успішний.");
                    UserMenu(user);
                }
                else
                {
                    Console.WriteLine("Невірний логін або пароль.");
                }
            }
        }

        private static void UserMenu(User user)
        {
            int choice;
            do
            {
                Console.WriteLine($"\nМеню для {user.Username}:");
                Console.WriteLine("1. Додати фільм");
                Console.WriteLine("2. Видалити фільм");
                Console.WriteLine("3. Переглянути свої фільми");
                Console.WriteLine("4. Редагувати профіль");
                Console.WriteLine("5. Вийти");
                choice = int.Parse(Console.ReadLine());

                using (AppDbContext db = new AppDbContext())
                {
                    User updatedUser = db.Users.Include(u => u.Movies).FirstOrDefault(u => u.Id == user.Id);

                    switch (choice)
                    {
                        case 1:
                            AddMovie(db, updatedUser);
                            break;
                        case 2:
                            DeleteMovie(db, updatedUser);
                            break;
                        case 3:
                            ViewMovies(db, updatedUser);
                            break;
                        case 4:
                            EditProfile(db, updatedUser);
                            break;
                        case 5:
                            Console.WriteLine("Вихід з облікового запису.");
                            break;
                        default:
                            Console.WriteLine("Невірний вибір.");
                            break;
                    }
                }
            } while (choice != 5);
        }

        private static void AddMovie(AppDbContext db, User user)
        {
            Title movie = new Title();
            Console.Write("Назва фільму: ");
            movie.Name = Console.ReadLine();
            Console.Write("Рік виходу: ");
            movie.Year = int.Parse(Console.ReadLine());
            Console.Write("Опис: ");
            movie.Description = Console.ReadLine();
            movie.UserId = user.Id;

            db.Titles.Add(movie);
            db.SaveChanges();

            Console.WriteLine("Фільм додано.");
        }

        private static void DeleteMovie(AppDbContext db, User user)
        {
            var movies = db.Titles.Where(t => t.UserId == user.Id).ToList();
            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }

            Console.Write("Введіть ID фільму для видалення: ");
            int id = int.Parse(Console.ReadLine());

            Title movieToDelete = movies.FirstOrDefault(m => m.Id == id);

            if (movieToDelete != null)
            {
                db.Titles.Remove(movieToDelete);
                db.SaveChanges();
                Console.WriteLine("Фільм видалено.");
            }
            else
            {
                Console.WriteLine("Фільм не знайдено або він не ваш.");
            }
        }

        private static void ViewMovies(AppDbContext db, User user)
        {
            var movies = db.Titles.Where(t => t.UserId == user.Id).ToList();

            if (movies.Count == 0)
            {
                Console.WriteLine("У вас ще немає доданих фільмів.");
                return;
            }

            Console.WriteLine("Ваші фільми:");
            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }
        }

        private static void EditProfile(AppDbContext db, User user)
        {
            Console.Write("Нове ім'я користувача: ");
            user.Username = Console.ReadLine();
            Console.Write("Новий email: ");
            user.Email = Console.ReadLine();
            Console.Write("Новий пароль: ");
            user.Password = Console.ReadLine();

            db.Users.Update(user);
            db.SaveChanges();

            Console.WriteLine("Профіль оновлено.");
        }

        private static void ReadUsers(AppDbContext db)
        {
            foreach (var user in db.Users)
            {
                Console.WriteLine(user);
            }
        }
    }
}
