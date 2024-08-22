using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class Announcement
    {
        [Key]
        public int AnId { get; set; }
        public string Slug { get; set; }
        public int UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public string BodyType { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public string? Engine { get; set; }
        public string City { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string Summary { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public virtual List<Multimedia> Multimedia { get; set; }
        public virtual List<DriverAssistanceSystems> DriverAssistanceSystems { get; set; }
        public virtual List<Safety> Safety { get; set; }
        public virtual List<Performance> Performance { get; set; }
        public virtual List<Other> Other { get; set; }
        public virtual User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<AnnouncementImages> Images { get; set; }
        public virtual ICollection<FavoriteAnnouncements> FavoriteAnnouncements { get; set; }
    }
}
