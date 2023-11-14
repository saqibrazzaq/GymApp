using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("UserType")]
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserTypeId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum UserTypeNames
    {
        Lead = 1,
        Member = 2,
        Staff = 3
    }
}
