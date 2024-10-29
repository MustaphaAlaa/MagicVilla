using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Model.DTO;

public class RegisterRequistDTO
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Email { get; set; }

    [Required] public string ConfirmEmail { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string ConfrimPassword { get; set; }
}   