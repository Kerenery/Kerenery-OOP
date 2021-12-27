using System;

namespace ReportsApp.DAL.Tools
{
    public class ReportsDALException : Exception
    {
        public ReportsDALException()
        {
        }

        public ReportsDALException(string message)
            : base(message)
        {
        }

        public ReportsDALException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}