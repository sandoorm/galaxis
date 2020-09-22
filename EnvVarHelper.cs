using System;

namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        public static string GetGalaxisDbConnectionString() =>
            "User Id=" + Environment.GetEnvironmentVariable("POSTGRESQL_USER")
            + ";Password=" + Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD")
            + ";Port=5432"
            + ";Database=" + Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE")
            + ";";
    }
}