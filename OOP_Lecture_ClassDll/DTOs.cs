using Newtonsoft.Json;
using OOP_Lecture_ClassDll;
using OOP_Lecture_ClassDll.Models;
using System;
using System.Collections.Generic;

namespace OOP_Lecture_ClassDll.DTOs
{
    public class SupervisorDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
    }
    public class SectionDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("supervisor")]
        public SupervisorDTO Supervisor { get; set; }

        [JsonProperty("type")]
        public SectionType Type { get; set; }

        [JsonProperty("payment")]
        public int Payment { get; set; }

        [JsonProperty("lessons_per_month")]
        public int LessonsPerMonth { get; set; }

        [JsonProperty("students_count")]
        public int StudentsCount { get; set; }
    }
    public class ChildrenCreativityHouseDTO
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("sections")]
        public List<SectionDTO> Sections { get; set; }
    }
}