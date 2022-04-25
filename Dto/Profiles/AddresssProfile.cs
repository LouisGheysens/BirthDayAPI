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
    public class AddresssProfile : ProfileBase<AddressDto, Address>
    {
        public override void CreateMapping()
        {
            CreateMap<Address, AddressDto>()
                .ReverseMap();

            base.CreateMapping();

        }
    }
