using DataAccessLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public sealed class Stewardess : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("Crew")]
        public int CrewId { get; set; }
        public Crew Crew { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Stewardess s = obj as Stewardess;
            if (s == null)
                return false;

            return s.Id == this.Id && s.BirthDate == this.BirthDate && s.FirstName == this.FirstName && s.LastName == this.LastName && s.CrewId == this.CrewId;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} FirstName: {FirstName} LastName: {LastName} BirthDate: {BirthDate} CrewId: {CrewId}");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
