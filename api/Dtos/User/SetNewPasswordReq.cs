using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class SetNewPasswordReq
    {
        [Required(ErrorMessage = "New Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters for New password")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters for confirm password")]
        [Compare("NewPassword", ErrorMessage = "Confirm password must match with New password")]
        public string? ConfirmNewPassword { get; set; }
    }
}
