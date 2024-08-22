﻿namespace AutomovieApi.Models.Post

{
    public class AnnouncementCreateRequest
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public string BodyType { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public string? Engine { get; set; }
        public string? summary {  get; set; }
        public List<IFormFile> Images { get; set; }


        public List<int> MultimediaFeatures { get; set; }
        public List<int> DriverAssistanceSystemsFeatures { get; set; }
        public List<int> SafetyFeatures { get; set; }
        public List<int> PerformanceFeatures { get; set; }
        public List<int> OtherFeatures { get; set; }

    }

}
