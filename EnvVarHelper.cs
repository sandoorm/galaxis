namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        // "User Id=galaxis-user;Password=galaxis-pw;Server=172.30.77.30;Port=5432;Database=sampledb1-galaxis;";
        public static string GetGalaxisDbConnectionString() =>
            "User Id=galaxis;Password=galaxis;Server=localhost;Port=5432;Database=galaxis;";
    }
}