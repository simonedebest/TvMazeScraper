using AutoMapper;
using TvMazeScraper.Api.Entities;
using CastMember = TvMaze.Connector.Models.CastMember.CastMember;
using Show = TvMaze.Connector.Models.Show.Show;

namespace TvMazeScraper.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Show, Models.Show>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(s => s.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
            
            CreateMap<Models.Show, ShowEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(s => s.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ShowEntity, Models.Show>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.ShowId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(dst => dst.Cast, opt => opt.MapFrom(s => s.Cast))
                
                .ForAllOtherMembers(opt => opt.Ignore());
            
            CreateMap<CastMember, Models.CastMember>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.Person.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(s => s.Person.Name))
                .ForMember(dst => dst.Birthday, opt => opt.MapFrom(s => s.Person.Birthday))
                .ForAllOtherMembers(opt => opt.Ignore());
            
            CreateMap<Models.CastMember, CastMemberEntity>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(dst => dst.Birthday, opt => opt.MapFrom(s => s.Birthday))
                .ForAllOtherMembers(opt => opt.Ignore());
            
            CreateMap<CastMemberEntity, Models.CastMember>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(dst => dst.Birthday, opt => opt.MapFrom(s => s.Birthday))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
