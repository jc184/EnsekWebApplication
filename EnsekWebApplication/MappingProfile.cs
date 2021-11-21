using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace EnsekWebApplication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<MeterReading, MeterReadingDTO>();
            CreateMap<AccountForCreationDTO, Account>();
            CreateMap<AccountForUpdateDTO, Account>();
            CreateMap<MeterReadingsForCreationDTO, MeterReading>();
            CreateMap<MeterReadingsForUpdateDTO, MeterReading>();
        }
    }
}
