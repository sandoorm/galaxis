namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        // "User Id=galaxis-user;Password=galaxis-pw;Server=172.30.50.48;Port=5432;Database=sampledb2-galaxis;";
        // "User Id=galaxis;Password=galaxis;Server=localhost;Port=5432;Database=galaxis;";
        public static string GetGalaxisDbConnectionString() =>
            "User Id=galaxis-user;Password=galaxis-pw;Server=172.30.83.184;Port=5432;Database=sampledb01;";
    }
}