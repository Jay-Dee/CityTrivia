using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CountryGetModel {
        [DataMember] public int Id { get; }
        [DataMember] public string? Name { get; }


        public CountryGetModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
