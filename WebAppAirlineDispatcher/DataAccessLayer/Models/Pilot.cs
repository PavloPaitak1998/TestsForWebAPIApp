using DataAccessLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public sealed class Pilot : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Experience { get; set; }

        public Crew Crew { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Pilot p = obj as Pilot;
            if (p == null)
                return false;

            return p.Id == this.Id && p.BirthDate == this.BirthDate && p.FirstName == this.FirstName && p.LastName == this.LastName && p.Experience == this.Experience;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} FirstName: {FirstName} LastName: {LastName} BirthDate: {BirthDate} Experience: {Experience}");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
