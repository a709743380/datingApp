using System.ComponentModel.DataAnnotations;

namespace API.Model;

public class RegisterDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public string Pwd { get; set; }

}
