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
            CreateMap<Person, PersonDto>()
                .ForMember(x => x.City, option => option.MapFrom(c => c.Address.City))
                .ForMember(x => x.Street, option => option.MapFrom(c => c.Address.Street))
                .ForMember(x => x.PostalCode, option => option.MapFrom(c => c.Address.PostalCode))
                .ForMember(x => x.Number, option => option.MapFrom(c => c.Address.Number))
                .ReverseMap();

            base.CreateMapping();

        }
    }
}
