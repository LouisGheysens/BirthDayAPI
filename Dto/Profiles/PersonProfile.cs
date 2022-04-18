using Data.Models;
using Dto.Base;
using Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Profiles
{
    public class PersonProfile : ProfileBase<PersonDto, Person>
    {
        public override void CreateMapping()
        {
            CreateMap<Person, PersonDto>().ReverseMap();

            base.CreateMapping();

        }
    }
}
