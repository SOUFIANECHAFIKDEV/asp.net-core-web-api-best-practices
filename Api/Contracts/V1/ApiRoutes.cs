namespace Api.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "api";

        public const string Base = "api";

        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Update = Base + "/posts/{postId}";
            public const string Get = Base + "/posts/{postId}";
            public const string Create = Base + "/posts";
            public const string Delete = Base + "/posts/{postId}";
        }
    }
}
