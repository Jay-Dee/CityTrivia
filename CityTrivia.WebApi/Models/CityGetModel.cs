using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CityGetModel {
        [DataMember] public int Id { get; }
        [DataMember] public string? Name { get; }
        [DataMember] public string? Description { get; }

        public CityGetModel(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
