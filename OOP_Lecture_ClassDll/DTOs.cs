using Newtonsoft.Json;
using OOP_Lecture_ClassDll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOP_Lecture_ClassDll.DTOs
{
    public class SupervisorDTO
    {
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
        public UserDTO User { get; set; }
    }
    public class SectionDTO
    {
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }

        public SupervisorDTO Supervisor { get; set; }

        public SectionType Type { get; set; }

        [Range(0, 10000)]
        public int Payment { get; set; }

        [Range(0, 10000)]
        [JsonProperty("lessons_per_month")]
        public int LessonsPerMonth { get; set; }

        [Range(0, 10000)]
        [JsonProperty("students_count")]
        public int StudentsCount { get; set; }
    }
    public class ChildrenCreativityHouseDTO
    {
        [MinLength(10)]
        [MaxLength(100)]
        public string Address { get; set; }
        public List<SectionDTO> Sections { get; set; } = new List<SectionDTO>();
    }

    public class UserDTO
    {
        public Guid Id { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        public string Username { get; set; }

        [RegularExpression("^[A-Za-z0-9+_.-]+@(.+)$")]
        public string Email { get; set; }

        [RegularExpression("")]
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}