using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class GenderRes
    {
        public int GenderId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
