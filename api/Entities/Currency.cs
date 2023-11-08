using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Currency")]
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        [Required, MinLength(3), MaxLength(3)]
        public string? Code { get; set; }
        [Required, MinLength(3)]
        public string? Name { get; set; }
    }
}
