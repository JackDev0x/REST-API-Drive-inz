using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class BodyType
    {
        [Key]
        public int BodyTypeID { get; set; }
        public string Type { get; set; }

    }
}
