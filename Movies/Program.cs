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
                Console.WriteLine("Menu:");
                Console.WriteLine("1. User registration");
                Console.WriteLine("2. View existing users");
                Console.WriteLine("3. Exit");
                choose = int.Parse(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        RegUser(db);
                        Console.WriteLine("");
                        break;
                    case 2:
                        ReadUsers(db);
                        Console.WriteLine("");
                        break;
                    case 3:
                        return;
                    default:
                        break;
                }

            } while (choose != 3);
        }
        private static void RegUser(AppDbContext db)
        {
            User user = new User();
            Console.WriteLine("Введіть ім'я користувача: ");
            user.Name = Console.ReadLine();
            Console.WriteLine("Введіть логін користувача: ");
            user.Login = Console.ReadLine();
            Console.WriteLine("Введіть пароль користувача: ");
            user.Password = Console.ReadLine();
            db.Users.Add(user);
            db.SaveChanges();
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
