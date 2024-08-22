using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class FuelType
    {
        [Key]
        public int FuelTypeID { get; set; }
        public string Type { get; set; }
    }
}
