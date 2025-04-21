using CmsApp.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsApp.Pages.Gallery.Management
{
    [Authorize(CmsAppPermissions.GalleryImage.Management)]
    public class ManagementModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
