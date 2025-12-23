using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreLoginWithSession.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="UserName is required ")]
        [Display(Name ="User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required ")]
        [Display(Name = "Password")]
        //[DataType(DataType.Password)]
        public string? Password { get; set; }

        [NotMapped]
        [Display(Name ="Confirm password")]
        [Required(ErrorMessage ="Reenter the password")]
        [Compare("Password",ErrorMessage ="password and confirm password must same")]
        //[DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required ")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage ="Address is required")]
        [Display(Name ="Address")]
        public string? UserAdd { get; set; }

        [Required(ErrorMessage ="Age is Required")]
        [Range(18,100,ErrorMessage ="Age Should be 18 above")]
        [Display(Name="Age")]
        public string? UserAge {  get; set; }
    }
}
