using BusinessLogicLayer.Services;
using BusinessLogicLayerTests.Fake;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FakeItEasy;
using NUnit.Framework;
using Shared.DTO;
using Shared.Exceptions;
using System;

namespace BusinessLogicLayerTests.Services
{
    [TestFixture]
    public class DepartureServiceTests
    {
        [Test]
        public void CreateEntity_Should_Create_departure_typeof_Departure()
        {
            // Arrange
            DepartureDTO departureDTO = new DepartureDTO
            {
                Id=1,
                CrewId=1,
                FlightId=1,
                FlightNumber=1111,
                PlaneId=1,
                Time=new DateTime(2018,07,12)
            };
            Departure departure = new Departure
            {
                Id = 1,
                CrewId = 1,
                FlightId = 1,
                FlightNumber = 1111,
                PlaneId = 1,
                Time = new DateTime(2018, 07, 12)
            };

            var departureRepository = new FakeRepository<Departure>();
            var departureService = new DepartureService(departureRepository);

            // Act
            departureService.CreateEntity(departureDTO);
            var result = departureRepository.Get(1);

            // Assert
            Assert.AreEqual(departure, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_departure_typeof_Departure()
        {
            // Arrange
            DepartureDTO departureDTO = new DepartureDTO
            {
                Id = 1,
                CrewId = 1,
                FlightId = 1,
                FlightNumber = 1111,
                PlaneId = 1,
                Time = new DateTime(2018, 07, 12)
            };
            Departure departure = new Departure
            {
                Id = 1,
                CrewId = 1,
                FlightId = 1,
                FlightNumber = 1111,
                PlaneId = 1,
                Time = new DateTime(2018, 07, 12)
            };

            var departureRepository = A.Fake<IRepository<Departure>>();
            A.CallTo(() => departureRepository.Get(A<int>._)).Returns(new Departure { Id = 1 });

            var departureService = new DepartureService(departureRepository);

            //Act 
            departureService.UpdateEntity(1, departureDTO);
            var result = departureRepository.Get(1);


            // Assert
            Assert.AreEqual(departure, result);
        }


        [Test]
        public void UpdateEntity_When_departure_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            DepartureDTO departureDTO = new DepartureDTO
            {
                Id = 1,
                CrewId = 1,
                FlightId = 1,
                FlightNumber = 1111,
                PlaneId = 1,
                Time = new DateTime(2018, 07, 12)
            };

            var departureRepository = A.Fake<IRepository<Departure>>();
            A.CallTo(() => departureRepository.Get(A<int>._)).Returns(null);
            var departureService = new DepartureService(departureRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => departureService.UpdateEntity(1, departureDTO));

        }

    }
}
