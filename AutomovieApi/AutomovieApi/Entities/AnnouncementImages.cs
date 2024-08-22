using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class AnnouncementImages
    {
        [Key]
        public int ImageId { get; set; }
        public int AnId { get; set; }
        public string ImageUrl { get; set; }
        public virtual Announcement Announcement { get; set; }
    }
}
