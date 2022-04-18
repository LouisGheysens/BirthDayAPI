using AutoMapper;
using Data.Interfaces;
using Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Base
{
    public abstract class ProfileBase<T, U> : Profile where T : IDto where U : IEntity
    {
        public ProfileBase() { CreateMapping(); }

        public virtual void CreateMapping()
        {
            CreateMap<T, U>().ReverseMap();
        }
    }
}
