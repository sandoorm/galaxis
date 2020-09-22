using System;

namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        //"User Id=" + Environment.GetEnvironmentVariable("POSTGRESQL_USER")
        //    + ";Password=" + Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD")
        //    + ";Server=postgresql"
        //    + ";Port=5432"
        //    + ";Database=" + Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE")
        //    + ";";

        public static string GetGalaxisDbConnectionString() =>
            "User Id=galaxis-user;Password=galaxis-pw;Server=172.30.77.30;Port=5432;Database=sampledb1-galaxis;";
    }
}