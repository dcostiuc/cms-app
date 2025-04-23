namespace CmsApp.Permissions
{
    public static class CmsAppPermissions
    {
        public const string GroupName = "CmsApp";

        public static class Pages
        {
            public const string Root = GroupName + ".Pages";

            public const string Management = Root + ".Management";
            public const string Create = Root + ".Create";
            public const string Edit = Root + ".Edit";
            public const string Delete = Root + ".Delete";
            public const string SetAsHomepage = Root + ".SetAsHomepage";
        }
    }
}
