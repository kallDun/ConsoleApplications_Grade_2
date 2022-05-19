using System.Linq;
using OOP_Lecture_ClassDll.DTOs;
using OOP_Lecture_ClassDll.Models;

namespace OOP_Lecture_ClassDll.Adapters
{
    public interface IAdapter<T1, T2>
    {
        T1 ConvertToModel(T2 dto);
        T2 ConvertToDTO(T1 model);
    }
    public class SupervisorAdapter : IAdapter<Supervisor, SupervisorDTO>
    {
        private IAdapter<User, UserDTO> adapter = new UserAdapter();

        public SupervisorDTO ConvertToDTO(Supervisor model) => new SupervisorDTO
        {
            Name = model.Name,
            Surname = model.Surname,
            Birthday = model.Birthday,
            User = adapter.ConvertToDTO(model.User)
        };
        public Supervisor ConvertToModel(SupervisorDTO dto) => new Supervisor(dto.Name, dto.Surname, dto.Birthday, adapter.ConvertToModel(dto.User));
    }
    public class SectionAdapter : IAdapter<Section, SectionDTO>
    {
        private IAdapter<Supervisor, SupervisorDTO> adapter = new SupervisorAdapter();

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
    public class ChildrenCreativityHouseAdapter : IAdapter<ChildrenCreativityHouse, ChildrenCreativityHouseDTO>
    {
        private IAdapter<Section, SectionDTO> adapter = new SectionAdapter();

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

    public class UserAdapter : IAdapter<User, UserDTO>
    {
        public UserDTO ConvertToDTO(User model)
        {
            return new UserDTO
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password,
                Salt = model.Salt
            };
        }

        public User ConvertToModel(UserDTO dto)
        {
            var user = new User(dto.Id, dto.Username, dto.Email, dto.Phone, dto.Password, dto.Salt);
            return user;
        }
    }
}