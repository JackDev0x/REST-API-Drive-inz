using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class FavoriteAnnouncements
    {
        [Key]
        public int FavoriteAnnouncementId { get; set; }
        public int UserId { get; set; }
        public int AnnouncementAnId { get; set; }
        public virtual User User { get; set; }
        public virtual Announcement Announcement { get; set; }
    }
}