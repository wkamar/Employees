using AutoMapper;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Models.Employee, Data.Models.Employee>();
            CreateMap<Data.Models.Employee, Models.Employee>();

            CreateMap<Models.Department, Data.Models.Department>();
            CreateMap<Data.Models.Department, Models.Department>();

            //CreateMissingTypeMaps = true;
        }

    }
}
