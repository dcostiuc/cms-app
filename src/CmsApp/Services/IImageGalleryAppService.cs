using CmsApp.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CmsApp.Services
{
    public interface IImageGalleryAppService : ICrudAppService<GalleryImageDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateGalleryImageDto, CreateUpdateGalleryImageDto>
    {
        Task<List<GalleryImageWithDetailsDto>> GetDetailedListAsync();
    }
}
