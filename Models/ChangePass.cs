using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreLoginWithSession.Models
{
    public class ChangePass
    {
        [Required]
        [Display(Name="Current Password")]
        public string? CurPass { get; set; }

        [Required]
        [Display(Name ="New Password")]
        public string? NewPass { get; set; }

        [Required]
        [Compare("NewPass")]
        [Display(Name ="Confirm Password")]
        public string? Confirmpass { get; set; }
    }
}
