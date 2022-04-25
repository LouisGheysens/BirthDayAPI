using Data.Interfaces;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Person: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Deleted { get; set; }
        public bool Present { get; set; }
        public string? LogoFileName { get; set; }
        public int AdresId { get; set; }
        public virtual Address? Address { get; set; }
    }
}
