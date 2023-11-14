using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("LeadStatus")]
    public class LeadStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LeadStatusId { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public enum LeadStatusNames
    {
        NewLead = 1,
        Attempted = 2,
        Contacted = 3,
        InDiscussion = 4,
        Converted = 5,
        Disqualified = 6
    }
}
