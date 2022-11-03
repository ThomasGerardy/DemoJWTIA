using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoJWTIA.Models
{
    
    public class MessageViewModel
    {
        [DisplayName("Votre message ")]
        [Required]
        [MinLength(1, ErrorMessage = "Message trop court")]
        [MaxLength(400, ErrorMessage = "Message trop long")]
        public string Content { get; set; }

    }
}
