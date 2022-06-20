using System;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineNumber = 0;
            using (ApplicationContext db = new ApplicationContext())
            {
                //читаем эксель
                using (StreamReader reader = new StreamReader(@"C:\Source\User_1.csv"))
                {
                    // построчно заносим данные в бд
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (lineNumber != 0)
                        {
                            var values = line.Split(',');
                            User user = new User { Date = DateTime.Parse(values[0]), Name = values[1], Lastname = values[2], Patro = values[3], City = values[4], Country = values[5] };
                            db.Users.Add(user);
                            db.SaveChanges();
                        }
                        lineNumber++;
                    }
                    Console.WriteLine("Объекты успешно сохранены");
                    Console.Write("Выберите пункт меню:\n1.Поиск пользователя\n2.Удаление записей\n");
                    //логика для меню выбора
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Write("Введите фамилию пользователя либо её часть: ");
                            string? lastname = Console.ReadLine();
                            var users = db.Users.Where(p => EF.Functions.Like(p.Lastname, $"%{lastname}%"));
                            // если users что-нибудь содержит, то выводим полученный список совпадений, если нет - то говорим, что ничего не найдено
                            if (users.Any()) 
                            {
                                foreach (User user in users)
                                    Console.WriteLine($"{user.Date} {user.Name} {user.Lastname} {user.Patro} {user.City} {user.Country}");
                            } else {
                                Console.WriteLine("По заданному критерию ничего не найдено");
                            }
                            break;
                        case "2":
                            var userslist = db.Users.ToList();
                            Console.WriteLine("Список пользователей:");
                            foreach (User u in userslist)
                            {
                                Console.WriteLine($"{u.Id} {u.Date} {u.Name} {u.Lastname} {u.Patro} {u.City} {u.Country}");
                            }
                            Console.Write("Введите номер записи, которую хотите удалить: ");
                            string? delid = Console.ReadLine();
                            var intid = Int64.Parse(delid); //приводим прочитанное из консоли значение к типу int
                            User deluser = db.Users.FirstOrDefault(x => x.Id == intid);
                            // если есть юзер с указанным id, то удаляем его и выводим получившийся список, если нет, то сообщение об ошибке удаления
                            if (deluser != null)
                            {
                                db.Users.Remove(deluser);
                                db.SaveChanges();
                                Console.WriteLine("Запись успешно удалена, ниже показан обновлённый список:");
                                var userss = db.Users.ToList();
                                foreach (User u in userss)
                                {
                                    Console.WriteLine($"{u.Id} {u.Date} {u.Name} {u.Lastname} {u.Patro} {u.City} {u.Country}");
                                }
                            } else {
                                Console.WriteLine("Пользователя с указананным Id нет в базе");
                            }
                            break;
                        default:
                            Console.WriteLine("Выберите предложенные пункты меню (1 или 2)");
                            break;
                    }
                    Console.WriteLine("Нажмите любую клавишу, чтобы закрыть приложение...");
                    Console.ReadKey();
                }
            }
        }
    }
}
