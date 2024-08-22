using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class OtherDataset
    {
        [Key]
        public int Id { get; set; }
        public string feature { get; set; }
    }
}