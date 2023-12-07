namespace api.Dtos.User
{
    public class UserRes
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public string? GenderId { get; set; }
        public GenderRes? Gender { get; set; }
    }
}
