using CmsApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CmsApp.Permissions
{
    public class CmsAppPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var pagesGroup = context.AddGroup(CmsAppPermissions.Pages.Root, L("Permission:Pages"));

            pagesGroup.AddPermission(CmsAppPermissions.Pages.Management, L("Permission:Management"));
            pagesGroup.AddPermission(CmsAppPermissions.Pages.Create, L("Permission:Create"));
            pagesGroup.AddPermission(CmsAppPermissions.Pages.Edit, L("Permission:Edit"));
            pagesGroup.AddPermission(CmsAppPermissions.Pages.Delete, L("Permission:Delete"));
            pagesGroup.AddPermission(CmsAppPermissions.Pages.SetAsHomepage, L("Permission:SetAsHomepage"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmsAppResource>(name);
        }
    }
}
