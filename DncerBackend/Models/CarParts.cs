

namespace DncerBackend.Models
{
    public class CarParts
    {
        
        public int? Id { get; set; } // Primary key, set to int for database compatibility
        public string? PartName { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; } // Optional, can be used for specific part models

        // Foreign key to Cars table
        public int? CarId { get; set; }

        // Navigation property for one-to-many relationship (optional, depending on your needs)
        public Cars? Car { get; set; }
        public string? Currency { get; set; }
    }
}
