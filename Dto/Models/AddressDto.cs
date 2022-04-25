using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models
{
    public class AddressDto: IDto
    {
        public int Id { get; set; }
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }

        public string? Number { get; set; }
    }
}
