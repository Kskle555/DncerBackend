using System.ComponentModel.DataAnnotations;

namespace DncerBackend.Models
{
    public class Cars
    {
        [Key]
        public int? Id { get; set; }
        public string? CarName { get; set; }
        public int? CarYear { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? EngineType { get; set; }
        public string? TransmissionType { get; set; }
        public int? Mileage { get; set; }
        public string? LicensePlate { get; set; }
        public string? ChassisNumber { get; set; }
        public double? EngineCapacity { get; set; }
        public int? Horsepower { get; set; }
        public double? Torque { get; set; }
        public double? Acceleration { get; set; }
        public double? UrbanFuelConsumption { get; set; }
    }

}
