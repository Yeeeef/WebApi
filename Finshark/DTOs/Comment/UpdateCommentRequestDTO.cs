using System.ComponentModel.DataAnnotations;

namespace Finshark.DTO;

public class UpdateCommentRequestDTO
{
    [Required]
    [MinLength(3, ErrorMessage ="must be more than 3 characters")]
    [MaxLength(100, ErrorMessage ="Subject cannot be longer than 100 characters")]
    public string Subject { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage ="must be more than 3 characters")]
    [MaxLength(250,ErrorMessage ="Content must be less than 250 characters")]
    public string Content { get; set; } = string.Empty;
}
