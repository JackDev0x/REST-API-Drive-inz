using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public string Name { get; set; }

        // Foreign key
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}
