using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CityGetModel {
        [DataMember] public int Id { get; }
        [DataMember] public string? Name { get; }
        [DataMember] public string? Description { get; }
        [DataMember] public bool IsExistent { get; }
        [DataMember] public string CountryName { get; }


        public CityGetModel(int id, string name, string? description, bool isExistent, string countryName)
        {
            Id = id;
            Name = name;
            Description = description;
            IsExistent = isExistent;
            CountryName = countryName;
        }
    }
}
