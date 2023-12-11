using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("TimeUnit")]
    public class TimeUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TimeUnitId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum TimeUnitNames
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Year = 4
    }
}
