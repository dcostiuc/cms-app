using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace CmsApp.Pages.Shared.Components.Footer;

public class CmsAppFooterViewComponent : AbpViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("/Pages/Shared/Components/Footer/Default.cshtml");
    }
}