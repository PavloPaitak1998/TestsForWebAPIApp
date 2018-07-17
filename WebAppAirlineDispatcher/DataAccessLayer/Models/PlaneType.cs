
using DataAccessLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public sealed class PlaneType : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public int Seats { get; set; }
        public double Carrying { get; set; }

        public Plane Plane { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            PlaneType planeTypes = obj as PlaneType;
            if (planeTypes == null)
                return false;

            return planeTypes.Id == this.Id && planeTypes.Model == this.Model && planeTypes.Seats == this.Seats && planeTypes.Carrying == this.Carrying;
        }

        public override string ToString()
        {
            return string.Format($"Id: {Id} Model: {Model} Seats: {Seats} Carrying: {Carrying}");
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

    }
}
