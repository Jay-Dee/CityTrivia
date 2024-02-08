using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CityTrivia.WebApi.Models {
    [DataContract]
    public class CityPostModel {
        [Required(ErrorMessage = "You should provide a name value.")]
        [DataMember] public string Name { get; set; }
        [DataMember] public string? Description { get; set; }
        [DataMember] public bool IsExistent { get; set; }

        public CityPostModel(string name, string? description, bool isExistent) {
            Name = name;
            Description = description;
            IsExistent = isExistent;
        }
    }
}
