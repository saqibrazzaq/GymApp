using api.Entities;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class UserEditReq
    {
        public string FullName { get; set; } = "";
    }
}
