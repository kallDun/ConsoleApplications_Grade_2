using OOP_Lecture_ClassDll.Adapters;
using OOP_Lecture_ClassDll.DTOs;
using OOP_Lecture_ClassDll.Models;
using System;

namespace OOP_Lecture_Example1.Logic
{
    class Client
    {
        private ChildrenCreativityHouse creativityHouse;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\serialized.json";

        public ChildrenCreativityHouse GenerateAndGetCCHouse()
        {
            creativityHouse = new ChildrenCreativityHouseGenerator().GenerateClass();
            return creativityHouse;
        }
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