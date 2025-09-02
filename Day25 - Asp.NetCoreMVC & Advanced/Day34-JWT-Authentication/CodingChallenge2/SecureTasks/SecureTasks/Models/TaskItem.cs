using System.ComponentModel.DataAnnotations;

namespace SecureTasks.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = "";

        [StringLength(2000)]
        public string? Description { get; set; }  // user input -> output encoded in views

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        [Required]
        public string OwnerId { get; set; } = ""; // ApplicationUser.Id
    }
}
