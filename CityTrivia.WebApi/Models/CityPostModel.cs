using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CityPostModel {
        [DataMember] public string Name { get; }
        [DataMember] public string? Description { get; }
        [DataMember] public bool IsExistent { get; }

        public CityPostModel(string name, string? description, bool isExistent) {
            Name = name;
            Description = description;
            IsExistent = isExistent;
        }
    }
}
