using AutoMapper;

namespace powerplant_coding_challenge_by_david_reina.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Domain.Fuels, powerplant_coding_challenge.Models.Fuels>().ReverseMap();
            CreateMap<Domain.Payload, powerplant_coding_challenge.Models.Payload>().ReverseMap();
            CreateMap<Domain.Powerplant, powerplant_coding_challenge.Models.Powerplant>().ReverseMap();
            CreateMap<Domain.ProductionResult, powerplant_coding_challenge.Models.ProductionResult>().ReverseMap();
        }
    }
}
