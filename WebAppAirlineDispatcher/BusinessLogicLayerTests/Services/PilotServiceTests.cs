using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayerTests.Fake;
using DataAccessLayer.Data;
using DataAccessLayer.Implementation.Repositories;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shared.DTO;
using Shared.Exceptions;
using System;
using System.Linq;

namespace BusinessLogicLayerTests.Services
{
    [TestFixture]
    public class PilotServiceTests
    {
        readonly IRepository<Pilot> _pilotRepository;
        readonly IPilotService _pilotService;
        readonly DispatcherContext dispatcherContext;

        public PilotServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DispatcherContext>();
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=DispatcherDB;integrated security=True;MultipleActiveResultSets=True;");

            dispatcherContext = new DispatcherContext(optionsBuilder.Options);

            _pilotRepository = new Repository<Pilot>(dispatcherContext);

            _pilotService = new PilotService(_pilotRepository);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            var pilot1 = new Pilot { FirstName = "TestName1", LastName = "TestName1", BirthDate = DateTime.Parse("12.07.1998"), Experience = 1 };
            var pilot2 = new Pilot { FirstName = "TestName2", LastName = "TestName2", BirthDate = DateTime.Parse("28.03.1990"), Experience = 2 };


            dispatcherContext.Pilots.AddRange(pilot1, pilot2);
            dispatcherContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            dispatcherContext.Pilots.RemoveRange(dispatcherContext.Pilots.Where(p => p.FirstName.Contains("TestName") || p.LastName.Contains("TestName")));
            dispatcherContext.SaveChanges();
            _pilotRepository.Dispose();
        }

        //Task1
        [Test]
        public void CreateEntity_Should_Create_pilot_typeof_Pilot()
        {
            // Arrange
            PilotDTO pilotDTO = new PilotDTO
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Henk",
                Experience = 10,
                BirthDate = new DateTime(1998, 07, 12)
            };
            Pilot pilot = new Pilot
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Henk",
                Experience = 10,
                BirthDate = new DateTime(1998, 07, 12)
            };

            var pilotRepository = new FakeRepository<Pilot>();
            var pilotService = new PilotService(pilotRepository);

            // Act
            pilotService.CreateEntity(pilotDTO);
            var result = pilotRepository.Get(1);

            // Assert
            Assert.AreEqual(pilot, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_pilot_typeof_Pilot()
        {
            // Arrange
            PilotDTO pilotDTO = new PilotDTO
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Henk",
                Experience = 10,
                BirthDate = new DateTime(1998, 07, 12)
            };
            Pilot pilot = new Pilot
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Henk",
                Experience = 10,
                BirthDate = new DateTime(1998, 07, 12)
            };

            var pilotRepository = A.Fake<IRepository<Pilot>>();
            A.CallTo(() => pilotRepository.Get(A<int>._)).Returns(new Pilot { Id = 1 });

            var pilotService = new PilotService(pilotRepository);

            //Act 
            pilotService.UpdateEntity(1, pilotDTO);
            var result = pilotRepository.Get(1);

            // Assert
            Assert.AreEqual(pilot, result);
        }


        [Test]
        public void UpdateEntity_When_pilot_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            PilotDTO pilotDTO = new PilotDTO
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Henk",
                Experience = 10,
                BirthDate = new DateTime(1998, 07, 12)
            };

            var pilotRepository = A.Fake<IRepository<Pilot>>();
            A.CallTo(() => pilotRepository.Get(A<int>._)).Returns(null);
            var pilotService = new PilotService(pilotRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => pilotService.UpdateEntity(1, pilotDTO));

        }


        //Task2
        [Test]
        public void CreateEntity_Should_Create_pilot_in_db()
        {
            // Arrange
            PilotDTO pilotDTO = new PilotDTO
            {
                FirstName = "TestName4",
                LastName = "TestName4",
                BirthDate = DateTime.Parse("12.06.1994"),
                Experience = 10
            };

            // Act
            _pilotService.CreateEntity(pilotDTO);
            var pilotResult = dispatcherContext.Pilots
                .FirstOrDefault(c => c.FirstName == "TestName4" && c.LastName == "TestName4");

            // Assert
            Assert.IsTrue(pilotResult != null);
        }

        [Test]
        public void UpdateEntity_Should_Update_pilot_in_db()
        {
            // Arrange
            var pilot = dispatcherContext.Pilots
                .FirstOrDefault(c => c.FirstName == "TestName1" && c.LastName == "TestName1");

            PilotDTO pilotDTO = new PilotDTO
            {
                FirstName = "TestName1",
                LastName = "TestName1",
                BirthDate = DateTime.Parse("12.06.1994"),
                Experience = 20
            };

            pilot = new Pilot
            {
                Id = pilot.Id,
                FirstName = "TestName1",
                LastName = "TestName1",
                BirthDate = DateTime.Parse("12.06.1994"),
                Experience = 20
            };

            // Act
            _pilotService.UpdateEntity(pilot.Id, pilotDTO);
            var pilotResult = _pilotRepository.Get(pilot.Id);

            // Assert
            Assert.AreEqual(pilot, pilotResult);
        }

        [Test]
        public void GetEntity_Should_Get_pilot_by_id_from_db()
        {
            // Arrange
            var pilot = dispatcherContext.Pilots
                .FirstOrDefault(c => c.FirstName == "TestName1" && c.LastName == "TestName1");

            // Act
            var pilotResult = _pilotService.GetEntity(pilot.Id);

            // Assert
            Assert.IsTrue(pilotResult != null);
        }

        [Test, Order(4)]
        public void GetEntity_Should_throw_ValidationException_When_pilot_with_id_doesnt_exist_in_db()
        {
            // Arrange

            // Assert and Act
            Assert.Throws<ValidationException>(() => _pilotService.GetEntity(5000000));
        }

        [Test]
        public void UpdateEntity_Should_throw_ValidationException_When_pilot_with_id_doesnt_exist_in_db()
        {
            // Arrange
            PilotDTO pilotDTO = new PilotDTO
            {
                FirstName = "TestName1",
                LastName = "TestName1",
                BirthDate = DateTime.Parse("12.07.1998"),
                Experience = 11
            };

            // Assert and Act
            Assert.Throws<ValidationException>(() => _pilotService.UpdateEntity(5000000, pilotDTO));
        }

        [Test]
        public void DeleteEntity_Should_throw_ValidationException_When_pilot_with_id_doesnt_exist_in_db()
        {
            // Arrange

            // Assert and Act
            Assert.Throws<ValidationException>(() => _pilotService.DeleteEntity(500000));
        }

        [Test]
        public void DeleteEntity_Should_Delete_pilot_by_id_from_db()
        {
            // Arrange
            var pilot = dispatcherContext.Pilots
                .FirstOrDefault(c => c.FirstName == "TestName2" && c.LastName == "TestName2");

            // Act
            _pilotService.DeleteEntity(pilot.Id);

            // Assert
            Assert.Throws<ValidationException>(() => _pilotService.GetEntity(pilot.Id));
        }

    }
}
