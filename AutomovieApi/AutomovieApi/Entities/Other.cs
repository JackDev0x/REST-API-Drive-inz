﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Entities
{
    public class Other
    {
        [Key]
        public int Id { get; set; }
        public int AnId { get; set; }
        public string feature { get; set; }
        public int featureId { get; set; }
        public virtual Announcement Announcement { get; set; }
        public virtual OtherDataset OtherDataset { get; set; }
    }
}