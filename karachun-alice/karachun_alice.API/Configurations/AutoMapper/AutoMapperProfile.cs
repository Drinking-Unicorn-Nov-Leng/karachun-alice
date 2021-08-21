using AutoMapper;
using AutoMapper.EquivalencyExpression;
using karachun_alice.Data;
using karachun_alice.Data.Entity;
using System;
using System.Linq;

namespace karachun_alice.API.Configurations.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<ClassDTO, ClassEntity>();
        }
    }
}
