using System.Collections.Generic;

namespace DesignTimeExample.Models
{
    public class Plane
    {
        public int Id { get; set; }
        public int SeatsNumber { get; set; }
        public PlaneModel Model { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }

    public enum PlaneModel
    {
        AIRBUS_A380,
        BOEING_707,
        AIRBUS_A320,
        BOEING_727,
        BOEING_767,
        BOEING_757,
        BOEING_787,
        BOEING_737,
        BOEING_777,
        BOEING_747
    }
}