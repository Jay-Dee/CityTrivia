using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityTrivia.DataAccess.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool IsExistent { get; set; } = true;

        [ForeignKey(nameof(Country))]
        [DefaultValue(1)]
        [Required]
        public int CountryId { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
