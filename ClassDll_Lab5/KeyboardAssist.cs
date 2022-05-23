using OOP_Lecture_ClassDll.Adapters;
using OOP_Lecture_ClassDll.DTOs;
using OOP_Lecture_ClassDll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassDll_Lab5
{
    class KeyboardAssist
    {
        GlobalRepository repository;
        public KeyboardAssist(GlobalRepository repository)
        {
            this.repository = repository;
        }

        public void ReadData()
        {
            Console.WriteLine("To input user data - write '/user', otherwise write '/next'");
            string input = Console.ReadLine();
            while (input is "/user")
            {
                try
                {
                    string username, email, phone;
                    Console.WriteLine("Write username:");
                    username = Console.ReadLine();
                    Console.WriteLine("Write email:");
                    email = Console.ReadLine();
                    Console.WriteLine("Write phone:");
                    phone = Console.ReadLine();
                    UserDTO user_dto = new UserDTO
                    {
                        Id = Guid.NewGuid(),
                        Username = username,
                        Email = email,
                        Phone = phone
                    };

                    var validations = new List<ValidationResult>();
                    if (!Validator.TryValidateObject(user_dto, new ValidationContext(user_dto), validations, true))
                    {
                        throw new Exception(string.Join(", ", validations));
                    };

                    User user = new UserAdapter().ConvertToModel(user_dto);
                    repository.AddUser(user);
                    Console.WriteLine($"Successfully created user with id: {user.Id}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong with exception: " + e.Message);
                }
                Console.WriteLine("To input next user data - write '/user', otherwise write '/next'");
                input = Console.ReadLine();
            }

            ChildrenCreativityHouse house;
            try
            {
                Console.WriteLine("Write address of Children Creativity House:");
                string address = Console.ReadLine();
                ChildrenCreativityHouseDTO house_dto = new ChildrenCreativityHouseDTO { Address = address };

                var validations = new List<ValidationResult>();
                if (!Validator.TryValidateObject(house_dto, new ValidationContext(house_dto), validations, true))
                {
                    throw new Exception(string.Join(", ", validations));
                };

                house = new ChildrenCreativityHouseAdapter().ConvertToModel(house_dto);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with exception: " + e.Message);
                return;
            }

            if (repository.Users.Count == 0)
            {
                Console.WriteLine("Cannot create supervisors with no users!");
                return;
            }

            Console.WriteLine("To input supervisor data - write '/supervisor', otherwise write '/next'");
            input = Console.ReadLine();
            while (input is "/supervisor")
            {
                try
                {
                    Console.WriteLine("Write user id:");
                    Guid userId = Guid.Parse(Console.ReadLine());
                    User user = repository.GetUserById(userId);
                    if (user is null) throw new Exception("Cannot find user with this id!");
                    if (repository.GetSupervisorByUserId(userId) != null) throw new Exception("Supervisor already exist with this user!");

                    string name, surname, birthday;
                    Console.WriteLine("Write user name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Write user surname:");
                    surname = Console.ReadLine();
                    Console.WriteLine("Write user birthday:");
                    birthday = Console.ReadLine();

                    SupervisorDTO supervisorDTO  = new SupervisorDTO
                    {
                        Name = name,
                        Surname = surname,
                        Birthday = DateTime.Parse(birthday),
                        User = new UserAdapter().ConvertToDTO(user)
                    };

                    var validations = new List<ValidationResult>();
                    if (!Validator.TryValidateObject(supervisorDTO, new ValidationContext(supervisorDTO), validations, true))
                    {
                        throw new Exception(string.Join(", ", validations));
                    };

                    Supervisor supervisor = new SupervisorAdapter().ConvertToModel(supervisorDTO);
                    repository.AddSupervisor(supervisor);                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong with exception: " + e.Message);
                }
                Console.WriteLine("To input next supervisor data - write '/supervisor', otherwise write '/next'");
                input = Console.ReadLine();
            }

            if (repository.Supervisors.Count == 0)
            {
                Console.WriteLine("Cannot create sections with no supervisors!");
                return;
            }

            Console.WriteLine("To input section data - write '/section', otherwise write '/next'");
            input = Console.ReadLine();
            while (input is "/section")
            {
                try
                {
                    Console.WriteLine("Write supervisor's user id:");
                    Guid userId = Guid.Parse(Console.ReadLine());
                    Supervisor supervisor = repository.GetSupervisorByUserId(userId);
                    if (repository is null) throw new Exception("Cannot find supervisor!");

                    string name;
                    SectionType type;
                    int payment;
                    int lessonsPerMonth;
                    int studentsCount;

                    Console.WriteLine("Write section name:");
                    name = Console.ReadLine();
                    Console.WriteLine($"Write section type ({string.Join(", ", Enum.GetNames(typeof(SectionType)))}):");
                    type = (SectionType)Enum.Parse(typeof(SectionType), Console.ReadLine());
                    Console.WriteLine("Write section payment:");
                    payment = int.Parse(Console.ReadLine());
                    Console.WriteLine("Write section lessons per month:");
                    lessonsPerMonth = int.Parse(Console.ReadLine());
                    Console.WriteLine("Write section students count:");
                    studentsCount = int.Parse(Console.ReadLine());

                    SectionDTO sectionDTO = new SectionDTO
                    {
                        Name = name,
                        Type = type,
                        Payment = payment,
                        LessonsPerMonth = lessonsPerMonth,
                        StudentsCount = studentsCount,
                        Supervisor = new SupervisorAdapter().ConvertToDTO(supervisor)
                    };

                    var validations = new List<ValidationResult>();
                    if (!Validator.TryValidateObject(sectionDTO, new ValidationContext(sectionDTO), validations, true))
                    {
                        throw new Exception(string.Join(", ", validations));
                    };

                    Section section = new SectionAdapter().ConvertToModel(sectionDTO);
                    house.AddSection(section);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong with exception: " + e.Message);
                }
                Console.WriteLine("To input next section data - write '/section', otherwise write '/next'");
                input = Console.ReadLine();
            }

            repository.House = house;
        }

        public void WriteDataByModel()
        {
            Console.WriteLine("Enter 'User' or 'Supervisor':");
            string input = Console.ReadLine();
            if (input == "User")
            {
                Console.WriteLine("Enter User Id:");
                try
                {
                    Guid id = Guid.Parse(Console.ReadLine());
                    User user = repository.GetUserById(id);
                    if (user is null) throw new Exception("Cannot find user!");
                    var (supervisor, list) = repository.FindRelativesByModel(user);
                    Console.WriteLine("Supervisor: " + supervisor);
                    Console.WriteLine("Sections: " + list);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Some error occured: " + e.Message);
                }
            }
            else
            if (input == "Supervisor")
            {
                try
                {
                    Guid id = Guid.Parse(Console.ReadLine());
                    Supervisor supervisor = repository.GetSupervisorByUserId(id);
                    if (supervisor is null) throw new Exception("Cannot find supervisor!");
                    var (user, list) = repository.FindRelativesByModel(supervisor);
                    Console.WriteLine("Supervisor: " + user);
                    Console.WriteLine("Sections: " + list);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Some error occured: " + e.Message);
                }
            }
        }
    }
}