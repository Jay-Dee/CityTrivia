using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CountryPostModel {
        [DataMember] public string? Name { get; }


        public CountryPostModel(string name)
        {
            Name = name;
        }
    }
}
