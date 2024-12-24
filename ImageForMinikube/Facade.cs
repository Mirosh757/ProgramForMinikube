using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageForMinikube
{
    internal class Facade
    {
        string _ipAddress;
        public Facade(string ipAddress)
        {
            _ipAddress = ipAddress;
        }

        private void UserCreate()
        {
            using (ApplicationContext db = new ApplicationContext(_ipAddress))
            {
                Console.Write("Введите значения атрибутов сущности\nName: ");
                string name = Console.ReadLine();
                Console.Write("Age: ");
                int age = Int32.Parse(Console.ReadLine());
                User user = new User() { Name = name, Age = age };
                db.Users.Add(user);
                db.SaveChanges();
            }
            PrintMenu();
        }
        private void UserRead()
        {
            using (ApplicationContext db = new ApplicationContext(_ipAddress))
            {
                var users = db.Users.ToList();
                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
                }
            }
            PrintMenu();
        }
        private void UserUpdate()
        {
            using (ApplicationContext db = new ApplicationContext(_ipAddress))
            {
                Console.WriteLine("Введите id объекта для изменения значений его атрибутов");
                
                User? user = db.Users.Find(Int32.Parse(Console.ReadLine()));
                if(user != null)
                {
                    Console.WriteLine("Введите значения атрибутов через пробел");
                    string line = Console.ReadLine();
                    string[] attributes = line.Split(' ');
                    user.Name = attributes[0];
                    user.Age = Int32.Parse(attributes[1]);
                    db.Users.Update(user);
                    db.SaveChanges();
                    //Данные после изменения
                    UserRead();
                }
                else
                    Console.WriteLine("Пользователя с данным id не существует в базе");
            }
            PrintMenu();
        }
        private void UserDelete()
        {
            using (ApplicationContext db = new ApplicationContext(_ipAddress))
            {
                Console.WriteLine("Введите id для удаления объекта");
                User? user = db.Users.Find(Int32.Parse(Console.ReadLine()));
                if(user != null)
                {
                    //Удаляем объект по id
                    db.Users.Remove(user);
                    db.SaveChanges();
                    //Данные после удаления
                    UserRead();
                }
                else
                    Console.WriteLine("Пользователя с данным id не существует в базе");
            }
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("0.    Вставить объект в базу\n1.    Вывод всех экземпляров модели\n2.    Изменение значений атрибутов модели по id\n3.    Удаление модели из базы по id");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "0" || line != "1" || line != "2" || line != "3"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "0": UserCreate(); break;
                    case "1": UserRead(); break;
                    case "2": UserUpdate(); break;
                    case "3": UserDelete(); break;
                }
            }
        }
    }
}
