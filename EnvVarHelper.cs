using System;

namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        // + ";Server=postgresql"
        public static string GetGalaxisDbConnectionString() =>
            "User Id=" + Environment.GetEnvironmentVariable("POSTGRESQL_USER")
            + ";Password=" + Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD")
            + ";Database=" + Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE")
            + ";";
    }
}