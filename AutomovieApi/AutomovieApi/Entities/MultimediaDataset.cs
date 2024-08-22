using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class MultimediaDataset
    {
        [Key]
        public int Id { get; set; }
        public string feature { get; set; }
    }
}