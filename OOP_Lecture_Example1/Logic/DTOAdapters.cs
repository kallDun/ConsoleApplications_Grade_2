using System.Linq;

namespace OOP_Lecture_Example1.Logic
{
    interface IAdapter<T1, T2>
    {
        T1 ConvertToModel(T2 dto);
        T2 ConvertToDTO(T1 model);
    }
    class SupervisorAdapter : IAdapter<Supervisor, SupervisorDTO>
    {
        public SupervisorDTO ConvertToDTO(Supervisor model) => new SupervisorDTO
        {
            Name = model.Name,
            Surname = model.Surname,
            Birthday = model.Birthday
        };
        public Supervisor ConvertToModel(SupervisorDTO dto) => new Supervisor(dto.Name, dto.Surname, dto.Birthday);
    }
    class SectionAdapter : IAdapter<Section, SectionDTO>
    {
        private SupervisorAdapter adapter = new SupervisorAdapter();

        public SectionDTO ConvertToDTO(Section model) => new SectionDTO
        {
            Name = model.Name,
            Supervisor = adapter.ConvertToDTO(model.Supervisor),
            Type = model.Type,
            Payment = model.Payment,
            LessonsPerMonth = model.LessonsPerMonth,
            StudentsCount = model.StudentsCount
        };
        public Section ConvertToModel(SectionDTO dto) 
            => new Section(dto.Name, adapter.ConvertToModel(dto.Supervisor), dto.Type, dto.Payment, dto.LessonsPerMonth, dto.StudentsCount);
    }
    class ChildrenCreativityHouseAdapter : IAdapter<ChildrenCreativityHouse, ChildrenCreativityHouseDTO>
    {
        private SectionAdapter adapter = new SectionAdapter();

        public ChildrenCreativityHouseDTO ConvertToDTO(ChildrenCreativityHouse model) => new ChildrenCreativityHouseDTO
        {
            Address = model.Address,
            Sections = model.Sections.Select(x => adapter.ConvertToDTO(x)).ToList()
        };

        public ChildrenCreativityHouse ConvertToModel(ChildrenCreativityHouseDTO dto)
        {
            var house = new ChildrenCreativityHouse(dto.Address);
            foreach (var item in dto.Sections)
            {
                house.AddSection(adapter.ConvertToModel(item));
            }
            return house;
        }
            
    }
}