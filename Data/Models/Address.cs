using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class Address: IEntity
    {
        public int Id { get; set; }
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }

        public string? Number { get; set; }
    }
}
