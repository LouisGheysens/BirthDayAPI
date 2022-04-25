using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models
{
    public class PersonDto: IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Present { get; set; }
        public string? LogoFileName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public string? Number { get; set; }

    }

    public interface IDto
    {
       int Id { get; set; }
    }
}
