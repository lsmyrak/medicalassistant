using System;

namespace MedicalAssistant.AspNetCommon.Exceptions
{
    public class MigrationFailedException : Exception
    {
        public MigrationFailedException(Exception innerException)
            : base("Migration failed. For further details, see InnerException.", innerException)
        {
        }
    }
}
