using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class UserTypeRes
    {
        public int UserTypeId { get; set; }
        public string? Name { get; set; }
    }
}
