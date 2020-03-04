using AutoMapper;
using Frugal.Dtos;
using Frugal.Models;

namespace Frugal.Mappings
{
    /// <summary>
    /// Defines the mappings between the Frugal types.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructs a new instance of the mapping profile.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Budget, BudgetDto>();
            CreateMap<BudgetDto, Budget>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Business, BusinessDto>();
            CreateMap<BusinessDto, Business>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}