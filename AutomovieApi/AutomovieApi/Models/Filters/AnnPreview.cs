namespace AutomovieApi.Models.Filters
{
    public class AnnPreview
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Slug { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public bool? Damaged { get; set; }
        public string Description { get; set; }
        public string summary { get; set; }
        public string? Engine { get; set; }
        public string FuelType { get; set; }
        public List<int> LikedBy { get; set; }
        public int ProductionYear { get; set; }
        public decimal Price { get; set; }
        public decimal? FuelConsumption { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public UserDto User { get; set; }
        public List<AnnouncementImagesDto> Images { get; set; }
    }

}
