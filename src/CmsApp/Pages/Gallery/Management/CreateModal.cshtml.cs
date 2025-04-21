using CmsApp.Permissions;
using CmsApp.Services;
using CmsApp.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsApp.Pages.Gallery.Management
{
    [Authorize(CmsAppPermissions.GalleryImage.Create)]
    public class CreateModalModel : PageModel
    {
        [BindProperty]
        public CreateUpdateGalleryImageDto Image { get; set; }

        private readonly IImageGalleryAppService _imageGalleryAppService;

        public CreateModalModel(IImageGalleryAppService imageGalleryAppService)
        {
            _imageGalleryAppService = imageGalleryAppService;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            await _imageGalleryAppService.CreateAsync(Image);
        }
    }
}
