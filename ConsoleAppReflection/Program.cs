using System;
using System.Linq;
using System.Reflection;
using OOP_Lecture_ClassDll.Models;

namespace ConsoleAppReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User(Guid.NewGuid(), "Username", "email@gmail.com", "0987654321", "1234", "dfseFfDFS342fs");

            Console.WriteLine(user);

            PropertyInfo property_email = user.GetType().GetProperty("Email");
            property_email.SetValue(user, "newEmail@gmail.com");

            Console.WriteLine(user);

            PropertyInfo property_id = user.GetType().GetProperty("Id");
            property_id.SetValue(user, Guid.NewGuid());

            var equals = user.GetType()
                .GetMethods()
                .First(x => x.Name == "Equals" && x.ReflectedType == user.GetType())
                .Invoke(user, new object[] { user });
            Console.WriteLine(equals);

            Console.WriteLine(user);
        }
    }
}