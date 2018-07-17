using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public sealed class Flight: IEntity
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string PointOfDeparture { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public DateTime DestinationTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public Departure Departure { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Flight f = obj as Flight;
            if (f == null)
                return false;

            return f.Id == this.Id && f.Number == this.Number && f.PointOfDeparture == this.PointOfDeparture && f.Destination == this.Destination && f.DepartureTime == this.DepartureTime && f.DestinationTime == this.DestinationTime;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} Number: {Number} PointOfDeparture: {PointOfDeparture} Destination: {Destination} DepartureTime: {DepartureTime} DestinationTime: {DestinationTime} ");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
