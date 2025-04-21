using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace CmsApp.Services.Dtos
{
    public class CreateUpdateGalleryImageDto
    {
        [NotNull]
        [StringLength(CmsAppConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        public Guid CoverImageMediaId { get; set; }
    }
}
