using chronovault_api.Helpers;

namespace chronovault_api.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; } 
        public string? Description { get; set; }
        public int Order { get; set; } 
        public int ProductId { get; set; } 
        public Product Product { get; set; }
        public string Url => $"{EnvironmentVariables.MinioBaseUrl}{FileName}";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}