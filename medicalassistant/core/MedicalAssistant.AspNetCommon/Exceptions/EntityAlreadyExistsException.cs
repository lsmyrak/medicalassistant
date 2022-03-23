using System;

namespace MedicalAssistant.AspNetCommon.Exceptions
{
    public class EntityAlreadyExistsException<TEntity> : CriticalException
    {
        public EntityAlreadyExistsException(Guid duplicateEntityId)
        : this(duplicateEntityId.ToString())
        {
        }

        public EntityAlreadyExistsException(string duplicatePrimaryKey)
            : base($"${nameof(TEntity)} insertion failed. An entity with the same ID ({duplicatePrimaryKey}) already exists.")
        {
        }
    }
}
