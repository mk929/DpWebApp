using System;

namespace DpWebApp.Util
{
    public static class ExceptionLogger
    {
        public static void WriteExceptionToConsole(Exception ex, DateTime now)
        {
            throw ex;
        }
    }
}