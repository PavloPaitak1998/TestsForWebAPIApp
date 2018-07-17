using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public sealed class Crew: IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Pilot")]
        public int PilotId { get; set; }
        public Pilot Pilot { get; set; }

        public ICollection<Stewardess> Stewardesses { get; set; }

        public Departure Departure { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Crew crew = obj as Crew;
            if (crew == null)
                return false;

            return crew.Id == this.Id && crew.PilotId == this.PilotId;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} PilotId: {PilotId}");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
