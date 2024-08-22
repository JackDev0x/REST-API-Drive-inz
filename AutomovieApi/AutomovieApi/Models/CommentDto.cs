using AutomovieApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Models
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int AnId { get; set; }

        [Required(ErrorMessage = "Tresc komentarza nie moze byc pusta")]
        public string CommentText { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public virtual AnnouncementDto Announcement { get; set; }
        public virtual UserDto User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
