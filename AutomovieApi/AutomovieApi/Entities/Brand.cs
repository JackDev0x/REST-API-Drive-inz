using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
