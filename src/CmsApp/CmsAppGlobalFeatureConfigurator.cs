using Volo.Abp.GlobalFeatures;
using Volo.Abp.Threading;

namespace CmsApp;

public class CmsAppGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            GlobalFeatureManager.Instance.Modules.CmsKit(cmsKit =>
            {
                cmsKit.Pages.Enable();
                cmsKit.Menu.Enable();
            });
        });
    }
}