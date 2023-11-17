using api.Entities;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class StaffEditReq
    {
        public string FullName { get; set; } = "";
    }
}
