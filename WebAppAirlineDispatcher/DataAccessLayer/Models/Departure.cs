using DataAccessLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public sealed class Departure: IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }

        public int FlightNumber { get; set; }
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        [ForeignKey("Crew")]
        public int CrewId { get; set; }
        public Crew Crew { get; set; }

        [ForeignKey("Plane")]
        public int PlaneId { get; set; }
        public Plane Plane { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Departure departure = obj as Departure;
            if (departure == null)
                return false;

            return departure.Id == this.Id && departure.Time == this.Time && departure.FlightNumber == this.FlightNumber && departure.FlightId == this.FlightId && departure.CrewId == this.CrewId && departure.PlaneId == this.PlaneId;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} Time: {Time} FlightNumber: {FlightNumber} FlightId: {FlightId} CrewId: {CrewId} PlaneId: {PlaneId}");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

    }
}
