namespace CmsApp.Permissions
{
    public static class CmsAppPermissions
    {
        public const string GroupName = "CmsApp";

        public static class GalleryImage
        {
            public const string Root = GroupName + ".GalleryImage";

            public const string Management = Root + ".Management";
            public const string Create = Root + ".Create";
            public const string Update = Root + ".Update";
            public const string Delete = Root + ".Delete";
        }
    }
}
