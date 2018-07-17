
using DataAccessLayer.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public sealed class Ticket : IEntity
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public int FlightNumber { get; set; }

        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Ticket ticket = obj as Ticket;
            if (ticket == null)
                return false;

            return ticket.Id == this.Id && ticket.Price == this.Price && ticket.FlightId == this.FlightId && ticket.FlightNumber == this.FlightNumber;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} Price: {Price} FlightId: {FlightId} FlightNumber: {FlightNumber}");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

    }
}
