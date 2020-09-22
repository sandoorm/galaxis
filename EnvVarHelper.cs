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
            "User Id=database-user;Password=database-password;Server=172.30.137.110;Port=5432;Database=database-name;";
    }
}