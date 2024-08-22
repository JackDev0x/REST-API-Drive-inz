using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class PerformanceDataset
    {
        [Key]
        public int Id { get; set; }
        public string feature { get; set; }
    }
}