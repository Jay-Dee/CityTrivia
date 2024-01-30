using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CityPostModel {
        [DataMember] public string Name { get; }
        [DataMember] public string? Description { get; }

        public CityPostModel(string name, string? description = null) {
            Name = name;
            Description = description;
        }
    }
}
