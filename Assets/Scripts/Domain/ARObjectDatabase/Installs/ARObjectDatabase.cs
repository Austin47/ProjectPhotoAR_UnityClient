using System;
using System.Collections.Generic;
using Infrastructure.DatabaseService;
using UnityEngine;
using Zenject;

namespace Domain.ARObjectDatabaseService
{
    public class ARObjectDatabase : IARObjectDatabase, IInitializable
    {
        [Serializable]
        public class ARObjectDataHolder
        {
            public List<ARObjectData> data = new List<ARObjectData>();
        }

        public List<ARObjectData> DefaultARObjects { get; private set; } = new List<ARObjectData>();
        //TODO: public List<ARObjectData> CustomARObjects { get; private set; } = new List<ARObjectData>();
        private IDatabase database;

        [Inject]
        public void Construct(IDatabase database)
        {
            this.database = database;
        }

        public void Initialize()
        {
            database.Load<ARObjectDataHolder>($"{Application.streamingAssetsPath}/DefaultARObjects.json", StoreDefaultARObjects);
        }

        private void StoreCustomARObjects(ARObjectDataHolder data)
        {
            // TODO: AT - Get CustomARObjects, 
            // also need to insure CustomARObjects.json exist on phone
            // Database.EnsureDirectoryExists?
            // if not create it
        } 

        private void StoreDefaultARObjects(ARObjectDataHolder data)
        {
            DefaultARObjects = data.data;
        }
    }
}


