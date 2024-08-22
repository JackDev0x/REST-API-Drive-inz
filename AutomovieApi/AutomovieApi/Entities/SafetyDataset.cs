using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class SafetyDataset
    {
        [Key]
        public int Id { get; set; }
        public string feature { get; set; }
    }
}