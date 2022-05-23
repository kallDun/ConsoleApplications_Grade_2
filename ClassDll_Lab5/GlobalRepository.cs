using OOP_Lecture_ClassDll.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassDll_Lab5
{
    class GlobalRepository
    {
        public ChildrenCreativityHouse House { get; set; }
        List<User> users = new List<User>();
        List<Supervisor> supervisors = new List<Supervisor>();
        public List<User> Users => users.ToList();
        public List<Supervisor> Supervisors => supervisors.ToList();

        public void AddUser(User user) => users.Add(user);
        public User GetUserById(Guid id) => users.FirstOrDefault(s => s.Id == id);

        public void AddSupervisor(Supervisor supervisor) => supervisors.Add(supervisor);
        public Supervisor GetSupervisorByUserId(Guid id) => supervisors.FirstOrDefault(s => s.User.Id == id);

        public (Supervisor, IEnumerable<Section>) FindRelativesByModel(User user)
        {
            Supervisor supervisor = supervisors.FirstOrDefault(x => x.User == user);
            IEnumerable<Section> list = House.Sections.Where(x => x.Supervisor == supervisor);
            return (supervisor, list);
        }
        public (User, IEnumerable<Section>) FindRelativesByModel(Supervisor supervisor)
        {
            User user = supervisor.User;
            IEnumerable<Section> list = House.Sections.Where(x => x.Supervisor == supervisor);
            return (user, list);
        }
    }
}