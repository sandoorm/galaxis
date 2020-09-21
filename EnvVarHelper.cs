using System;

namespace GalaxisProjectWebAPI
{
    public static class EnvVarHelper
    {
        public static string GetGalaxisDbConnectionString() => Environment.GetEnvironmentVariable("GALAXIS_CONNECTION_STRING_APP");
    }
}