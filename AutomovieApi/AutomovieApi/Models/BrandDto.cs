using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Models
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public ICollection<ModelDto> Models { get; set; }
    }
}
