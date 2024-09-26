using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class TriviaGetModel {
        [DataMember] public int Id { get; }
        [DataMember] public string? Information { get; }


        public TriviaGetModel(int id, string information)
        {
            Id = id;
            Information = information;
        }
    }
}
