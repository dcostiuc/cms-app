using Volo.Abp.Application.Dtos;

namespace CmsApp.Services.Dtos
{
    public class GalleryImageDto : CreationAuditedEntityDto<Guid>
    {
        public string Description { get; set; }
        public Guid CoverImageMediaId { get; set; }
    }
}
