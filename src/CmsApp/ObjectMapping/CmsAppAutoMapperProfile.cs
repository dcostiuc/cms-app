using AutoMapper;
using CmsApp.Entities;
using CmsApp.Services.Dtos;

namespace CmsApp.ObjectMapping;

public class CmsAppAutoMapperProfile : Profile
{
    public CmsAppAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */

        CreateMap<CreateUpdateGalleryImageDto, GalleryImage>().ReverseMap();

        CreateMap<GalleryImage, GalleryImageDto>().ReverseMap();

    }
}
