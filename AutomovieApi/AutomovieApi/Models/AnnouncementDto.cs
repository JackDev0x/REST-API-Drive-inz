using AutomovieApi.Entities;
using AutomovieApi.Services;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Models
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
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
        public double lan { get; set; }
        public double lng { get; set; }
        public string Summary { get; set; }
        public DateTime DatePosted { get; set; }
        public virtual UserDto User { get; set; }
        public virtual List<MultimediaDto> Multimedia { get; set; }
        public virtual List<DriverAssistanceSystemsDto> DriverAssistanceSystems { get; set; }
        public virtual List<SafetyDto> Safety { get; set; }
        public virtual List<PerformanceDto> Performance { get; set; }
        public virtual List<OtherDto> Other { get; set; }
        public virtual List<CommentDto> Comments { get; set; }
        public virtual List<AnnouncementImagesDto> Images { get; set; }
        public List<int> LikedBy { get; set; }

    }
}
