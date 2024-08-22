using AutomovieApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Proszę podać imię")]
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Proszę podać adres e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        public double lan { get; set; }
        public double lng { get; set; }
        public string? Voivodeship { get; set; }
        public string? City { get; set; }
        public virtual List<AnnouncementDto> Announcements { get; set; }
        public virtual List<FavoriteAnnouncementsDto> FavoriteAnnouncements { get; set; }
    }
}
