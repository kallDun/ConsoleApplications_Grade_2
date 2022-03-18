using OOP_Lecture_ClassDll;
using System;

namespace OOP_Lecture_Example1.Logic
{
    class Client
    {
        private ChildrenCreativityHouse creativityHouse;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\serialized.json";
        public Client()
        {
            Supervisor supervisor_ivan = new Supervisor("Ivan", "Ivanov", new DateTime(1990, 12, 10));
            Supervisor supervisor_vasiliy = new Supervisor("Vasiliy", "Ku", new DateTime(1995, 08, 17));
            creativityHouse = new ChildrenCreativityHouse("Some address");
            creativityHouse.AddSection(new Section("Beautiful Move", supervisor_ivan, SectionType.Dancing, payment: 100, lessonsPerMonth: 50, studentsCount: 100));
            creativityHouse.AddSection(new Section("Modeling Club", supervisor_vasiliy, SectionType.Modeling, payment: 160, lessonsPerMonth: 40, studentsCount: 50));
            creativityHouse.AddSection(new Section("Toy World", supervisor_ivan, SectionType.SoftToy, payment: 75, lessonsPerMonth: 20, studentsCount: 35));
            creativityHouse.AddSection(new Section("Magic Pen", supervisor_vasiliy, SectionType.Drawing, payment: 80, lessonsPerMonth: 60, studentsCount: 200));
        }

        public ChildrenCreativityHouse GetCCHouse() => creativityHouse;
        public ChildrenCreativityHouse GetCCHouseFromSerializator()
        {
            var adapter = new ChildrenCreativityHouseAdapter();
            var cchouse_dto = adapter.ConvertToDTO(creativityHouse);
            var serializator = new Serializator<ChildrenCreativityHouseDTO>(PATH);
            serializator.Serialize(cchouse_dto);
            var returned_dto = serializator.Deserialize();
            return adapter.ConvertToModel(returned_dto);
        }
    }
}