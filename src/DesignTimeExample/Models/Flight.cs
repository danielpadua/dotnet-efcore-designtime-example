using System.ComponentModel.DataAnnotations;

namespace DesignTimeExample.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required]
        public Plane Plane { get; set; }
        public int PassengerNumber { get; set; }
    }
}