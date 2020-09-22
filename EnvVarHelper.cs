using System;

namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        
        public static string GetGalaxisDbConnectionString() =>
            "User Id=" + Environment.GetEnvironmentVariable("POSTGRESQL_USER")
            + ";Password=" + Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD")
            + ";Server=postgresql"
            + ";Database=" + Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE")
            + ";";
    }
}