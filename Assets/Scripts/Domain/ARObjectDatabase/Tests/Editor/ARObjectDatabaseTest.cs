using System;
using System.Collections.Generic;
using Infrastructure.DatabaseService;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Domain.ARObjectDatabaseService.Tests
{
    public class ARObjectDatabaseTest
    {
        // load default Ar objects
        // TODO: AT - load custom Ar objects
        // TODO: AT - save Ar objects

        private DiContainer container;

        private IDatabase database;
        private ARObjectDatabase aRObjectDatabase;

        [Inject]
        private void Construct(ARObjectDatabase aRObjectDatabase)
        {
            this.aRObjectDatabase = aRObjectDatabase;
        }

        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<ARObjectDatabase>().AsSingle();

            database = Substitute.For<IDatabase>();
            container.Bind<IDatabase>().FromInstance(database);
            container.Inject(this);
        }

        [Test]
        public void LoadDefaultARObjects()
        {
            // Arrange
            ARObjectDatabase.ARObjectDataHolder data = new ARObjectDatabase.ARObjectDataHolder() { data = new List<ARObjectData>() };
            database.Load(Arg.Any<string>(), Arg.Do<Action<ARObjectDatabase.ARObjectDataHolder>>(x => x(data)));
            // Act
            aRObjectDatabase.Initialize();

            // Assert
            Assert.AreEqual(data.data, aRObjectDatabase.DefaultARObjects);
        }
    }
}


