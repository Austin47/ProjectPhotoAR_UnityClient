
using System.Collections.Generic;

namespace Domain.ARObjectDatabaseService
{
    public interface IARObjectDatabase
    {
        List<ARObjectData> DefaultARObjects { get; }
    }
}

