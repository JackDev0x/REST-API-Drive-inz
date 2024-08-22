using AutomovieApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Models
{
    public class ModelDto
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public BrandDto Brand { get; set; }
    }
}
