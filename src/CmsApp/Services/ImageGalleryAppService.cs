using CmsApp.Entities;
using CmsApp.Permissions;
using CmsApp.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Reactions;

namespace CmsApp.Services
{
    public class ImageGalleryAppService :
        CrudAppService<GalleryImage, GalleryImageDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateGalleryImageDto, CreateUpdateGalleryImageDto>,
        IImageGalleryAppService
    {
        public ImageGalleryAppService(IRepository<GalleryImage, Guid> repository) : base(repository)
        {
            CreatePolicyName = CmsAppPermissions.GalleryImage.Create;
            UpdatePolicyName = CmsAppPermissions.GalleryImage.Update;
            DeletePolicyName = CmsAppPermissions.GalleryImage.Delete;
        }

        public async Task<List<GalleryImageWithDetailsDto>> GetDetailedListAsync()
        {
            var dbContext = await Repository.GetDbContextAsync();

            var images = await (from image in dbContext.Set<GalleryImage>() 
                                select image).ToListAsync();

            return images.Select(x => new GalleryImageWithDetailsDto
            {
                Id = x.Id,
                Description = x.Description,
                CoverImageMediaId = x.CoverImageMediaId,

                CommentCount = (from comment in dbContext.Set<Comment>()
                                where comment.EntityType == CmsAppConsts.ImageGalleryEntityType && comment.EntityId == x.Id.ToString()
                                select comment).Count(),

                LikeCount = (from reaction in dbContext.Set<UserReaction>()
                             where reaction.EntityType == CmsAppConsts.ImageGalleryEntityType && reaction.EntityId == x.Id.ToString()
                             select reaction).Count()
            }).ToList();
        }
    }
}
