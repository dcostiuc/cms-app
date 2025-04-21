using CmsApp.Services;
using CmsApp.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsApp.Pages.Gallery
{
    public class DetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public GalleryImageDto Image { get; set; }

        private readonly IImageGalleryAppService _imageGalleryAppService;

        public DetailModel(IImageGalleryAppService imageGalleryAppService)
        {
            _imageGalleryAppService = imageGalleryAppService;
        }

        public async Task OnGetAsync()
        {
            Image = await _imageGalleryAppService.GetAsync(Id);
        }
    }
}
