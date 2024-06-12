using System.ComponentModel.DataAnnotations;

namespace Finshark.DTO;

public class RegisterDTO
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }
    [Required]
    public string? Password { get; set; }
}
