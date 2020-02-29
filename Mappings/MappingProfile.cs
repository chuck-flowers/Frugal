using AutoMapper;
using Frugal.Dtos;
using Frugal.Models;

namespace Frugal.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Budget, BudgetDto>();
            CreateMap<BudgetDto, Budget>();
        }
    }
}