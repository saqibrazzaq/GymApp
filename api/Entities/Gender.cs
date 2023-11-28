using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Gender")]
    public class Gender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }
        [Required, MinLength(1), MaxLength(1)]
        public string? Code { get; set; }
        [Required, MinLength(4)]
        public string? Name { get; set; }
    }

    public enum GenderNames
    {
        Male = 1,
        Female = 2,
        Unspecified = 3
    }

    public enum GenderCodes
    {
        M = 1,
        F = 2,
        U = 3
    }
}
