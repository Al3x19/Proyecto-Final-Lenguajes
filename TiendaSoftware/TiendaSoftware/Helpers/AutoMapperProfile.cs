using AutoMapper;
using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.DTOS.Dashboard;
using TiendaSoftware.DTOS.Lists;
using TiendaSoftware.DTOS.Publishers;
using TiendaSoftware.DTOS.Reviews;
using TiendaSoftware.DTOS.Softwares;
using TiendaSoftware.DTOS.Tags;
using TiendaSoftware.DTOS.Users;

namespace TiendaSoftware.Helpers
{
    public class AutoMapperProfile : Profile
    {


        public AutoMapperProfile()
        {
            MapsForPublishers();
            MapsForSoftwares();
            MapsForUsers();
            MapsForReviews();
            MapsForLists();
            MapsForTags();
            MapsForDashboard();
        }

        private void MapsForPublishers()
        {
            CreateMap<PublisherEntity, PublisherDto>();
            CreateMap<PublisherCreateDto, PublisherEntity>();
            CreateMap<PublisherEditDto, PublisherEntity>();
        }

        private void MapsForSoftwares()
        {
            CreateMap<SoftwareEntity, SoftwareDto>().ForMember(destino => destino.Tags, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.tags.Name).ToList()));
            CreateMap<SoftwareCreateDto, SoftwareEntity>();
            CreateMap<SoftwareEditDto, SoftwareEntity>();
        }

        private void MapsForTags()
        {
            CreateMap<TagEntity, TagDto>();
        }
        private void MapsForUsers()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserCreateDto, UserEntity>();
            CreateMap<UserEditDto, UserEntity>();
        }

        private void MapsForDashboard()
        {
            CreateMap<UserEntity, DashboardListDto>();
            CreateMap<SoftwareEntity, DashboardSoftwareDto>();
            CreateMap<TagEntity, DashboardTagDto>();
            CreateMap<ReviewEntity, DashboardReviewDto>();
        }
        private void MapsForLists()
        {
            CreateMap<ListEntity, ListDto>();
            CreateMap<ListCreateDto, ListEntity>();
            CreateMap<ListEditDto, ListEntity>();
        }

        private void MapsForReviews()
        {
            CreateMap<ReviewEntity, ReviewDto>();
            CreateMap<ReviewCreateDto, ReviewEntity>();
            CreateMap<ReviewEditDto, ReviewEntity>();
        }

    }
}
