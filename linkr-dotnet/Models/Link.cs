using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace linkr_dotnet.Models
{
    public class Link
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        
        [Required]
        public string Url { get; set; } = string.Empty;
    }
}
