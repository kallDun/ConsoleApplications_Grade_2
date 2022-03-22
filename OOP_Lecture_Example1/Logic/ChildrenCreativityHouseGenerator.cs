using OOP_Lecture_ClassDll.Models;
using System;
using System.Linq;

namespace OOP_Lecture_Example1.Logic
{
    class ChildrenCreativityHouseGenerator : AbstractGenerator<ChildrenCreativityHouse>
    {
        private readonly Random rnd = new Random();
        private readonly string[] supervisor_names =
            { "Andriy", "John", "Vasiliy", "Ihor", "Max", "Vitaliy", "Serhiy", "Vlad" };
        private readonly string[] supervisor_surnames =
            { "Ivanov", "Fedorov" };
        private readonly string[] section_names =
            { "MegaClub", "SuperInterestingClub", "YouWontRegretYourChoise" };
        private readonly string[] addresses =
            { "address" };

        public override ChildrenCreativityHouse GenerateClass()
        {
            var ccHouse = new ChildrenCreativityHouse(addresses[rnd.Next(addresses.Length)]);
            var sections = new Section[rnd.Next(10, 20)];
            var supervisors = new Supervisor[sections.Length / 3 * 2];
            supervisors = supervisors.Select(x => GenerateSupervisor()).ToArray();
            sections = sections.Select(x => GenerateSection(supervisors)).ToArray();
            foreach (var item in sections)
            {
                ccHouse.AddSection(item);
            }
            return ccHouse;
        }
        private Section GenerateSection(Supervisor[] supervisors)
        {
            var name = section_names[rnd.Next(section_names.Length)];
            var type = (SectionType)rnd.Next(Enum.GetNames(typeof(SectionType)).Length);
            var supervisor = supervisors[rnd.Next(supervisors.Length)];
            var payment = rnd.Next(50, 150);
            var students = rnd.Next(50, 250);
            var lessons_per_month = rnd.Next(20, 80);
            return new Section(name, supervisor, type, payment, lessons_per_month, students);
        }
        private Supervisor GenerateSupervisor()
        {
            var name = supervisor_names[rnd.Next(supervisor_names.Length)];
            var surname = supervisor_surnames[rnd.Next(supervisor_surnames.Length)];
            var year = rnd.Next(1970, DateTime.Now.Year - 20);
            var month = rnd.Next(1, 13);
            var day = rnd.Next(1, DateTime.DaysInMonth(year, month) + 1);
            var birthday = new DateTime(year, month, day);
            return new Supervisor(name, surname, birthday);
        }
    }
}