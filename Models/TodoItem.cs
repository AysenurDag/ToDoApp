using System.ComponentModel.DataAnnotations;

namespace todo_app.Models
{
    public class TodoItem
    {
        // Primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Title zorunludur.")]
        [MaxLength(200, ErrorMessage = "Title en fazla 200 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime? Due { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
