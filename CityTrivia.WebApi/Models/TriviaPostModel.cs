using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class TriviaPostModel {
        [DataMember] public string? Information { get; }


        public TriviaPostModel(string information)
        {
            Information = information;
        }
    }
}
