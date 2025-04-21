using CmsApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CmsApp.Permissions
{
    public class CmsAppPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CmsAppPermissions.GalleryImage.Root, L("Permission:ImageManagement"));

            myGroup.AddPermission(CmsAppPermissions.GalleryImage.Management, L("Permission:Management"));
            myGroup.AddPermission(CmsAppPermissions.GalleryImage.Create, L("Permission:Create"));
            myGroup.AddPermission(CmsAppPermissions.GalleryImage.Update, L("Permission:Edit"));
            myGroup.AddPermission(CmsAppPermissions.GalleryImage.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmsAppResource>(name);
        }
    }
}
