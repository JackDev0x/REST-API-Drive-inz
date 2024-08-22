using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class DriverAssistanceSystemsDataset
    {
        [Key]
        public int Id { get; set; }
        public string feature { get; set; }
    }
}