namespace SimpleAuthorizer.Common
{
    public static class IdentityConstants
    {
        public static class DefaultRoles
        {
            public const string CodeChallengeSuperAdmin = "CODE_CHALLENGE_SUPERUSER";
            public const string CodeChallengeAdmin = "CODE_CHALLENGE_ADMIN";
            public const string CodeChallengeRead = "CODE_CHALLENGE_READ";
            public const string CodeChallengeWrite = "CODE_CHALLENGE_WRITE";
            public const string CodeChallengeDelete = "CODE_CHALLENGE_DELETE";
        }

        public static class CustomClaims
        {
            public const string Admin = "Admin";
            public const string SuperAdmin = "SuperAdmin";

            public const string MovieRead = "Movie.Read";
            public const string MovieCreate = "Movie.Create";
            public const string MovieWrite = "Movie.Write";
            public const string MovieDelete = "Movie.Delete";

            public const string DirectorRead = "Director.Read";
            public const string DirectorCreate = "Director.Create";
            public const string DirectorWrite = "Director.Write";
            public const string DirectorDelete = "Director.Delete";

        }
    }
}
