using api.Entities;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class UserEditReq
    {
        public string FullName { get; set; } = "";
        public int? GenderId { get; set; }
    }
}
