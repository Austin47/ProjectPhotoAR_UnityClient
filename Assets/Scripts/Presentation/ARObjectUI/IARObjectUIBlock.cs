using Domain.ARObjectDatabaseService;
using UnityEngine;

namespace Presentation.ARObjectUI
{
    public interface IARObjectUIBlock
    {
        void Configure(Transform root, ARObjectData data);
    }
}
