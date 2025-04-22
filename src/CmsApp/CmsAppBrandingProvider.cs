using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CmsApp;

[Dependency(ReplaceServices = true)]
public class CmsAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CMS App";
}
