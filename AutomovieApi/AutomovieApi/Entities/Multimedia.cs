using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomovieApi.Entities
{
    public class Multimedia
    {
        [Key]
        public int Id { get; set; }

        public int AnId { get; set; }
        public string feature { get; set; }

        public int featureId { get; set; }
        public virtual Announcement Announcement { get; set; }
        public virtual MultimediaDataset MultimediaDataset { get; set; }


    }
}