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
    public class FlightServiceTests
    {
        readonly IRepository<Flight> _flightRepository;
        readonly IFlightService _flightService;
        readonly DispatcherContext dispatcherContext;

        public FlightServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DispatcherContext>();
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=DispatcherDB;integrated security=True;MultipleActiveResultSets=True;");

            dispatcherContext = new DispatcherContext(optionsBuilder.Options);

            _flightRepository = new Repository<Flight>(dispatcherContext);

            _flightService = new FlightService(_flightRepository);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            var flight1 = new Flight {
                Number = 1111,
                PointOfDeparture = "TestDeparture1",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "TestDestination1",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };
            var flight2 = new Flight { Number = 2222,
                PointOfDeparture = "TestDeparture2",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "TestDestination2",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };

            dispatcherContext.Flights.AddRange(flight1,flight2);
            dispatcherContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            dispatcherContext.Flights.RemoveRange(dispatcherContext.Flights
                .Where(f => f.PointOfDeparture == "TestDeparture4" || f.PointOfDeparture == "TestDeparture2" || f.PointOfDeparture == "TestDeparture1"));
            dispatcherContext.SaveChanges();
            _flightRepository.Dispose();
        }

        //Task1
        [Test]
        public void CreateEntity_Should_Create_flight_typeof_Flight()
        {
            // Arrange
            FlightDTO flightDTO = new FlightDTO
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };
            Flight flight = new Flight
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };

            var flightRepository = new FakeRepository<Flight>();
            var flightService = new FlightService(flightRepository);

            // Act
            flightService.CreateEntity(flightDTO);
            var result = flightRepository.Get(1);

            // Assert
            Assert.AreEqual(flight, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_flight_typeof_Flight()
        {
            // Arrange
            FlightDTO flightDTO = new FlightDTO
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };
            Flight flight = new Flight
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };
            var flightRepository = A.Fake<IRepository<Flight>>();
            A.CallTo(() => flightRepository.Get(A<int>._)).Returns(new Flight { Id = 1 });

            var flightService = new FlightService(flightRepository);

            //Act 
            flightService.UpdateEntity(1, flightDTO);
            var result = flightRepository.Get(1);

            // Assert
            Assert.AreEqual(flight, result);
        }


        [Test]
        public void UpdateEntity_When_flight_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            FlightDTO flightDTO = new FlightDTO
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };

            var flightRepository = A.Fake<IRepository<Flight>>();
            A.CallTo(() => flightRepository.Get(A<int>._)).Returns(null);

            var flightService = new FlightService(flightRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => flightService.UpdateEntity(1, flightDTO));
        }



        //Taks2
        [Test]
        public void CreateEntity_Should_Create_Flight_in_db()
        {
            // Arrange
            FlightDTO flightDTO = new FlightDTO
            {
                Number = 4444,
                PointOfDeparture = "TestDeparture4",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };

            // Act
            _flightService.CreateEntity(flightDTO);
            var flightResult = dispatcherContext.Flights.FirstOrDefault(f=>f.Number== 4444 && f.PointOfDeparture== "TestDeparture4");

            // Assert
            Assert.IsTrue(flightResult != null);

        }

        [Test]
        public void UpdateEntity_Should_Update_Flight_in_db()
        {
            // Arrange
            var flight = dispatcherContext.Flights.FirstOrDefault(f => f.Number == 1111 && f.PointOfDeparture== "TestDeparture1");
            FlightDTO flightDTO = new FlightDTO
            {
                Number = 1111,
                PointOfDeparture = "TestDeparture1",
                Destination = "Paris",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };

            flight = new Flight
            {
                Id = flight.Id,
                Number = 1111,
                PointOfDeparture = "TestDeparture1",
                Destination = "Paris",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };

            // Act

            _flightService.UpdateEntity(flight.Id, flightDTO);
            var flightResult = _flightRepository.Get(flight.Id);

            // Assert
            Assert.AreEqual(flight, flightResult);
        }

        [Test]
        public void GetEntity_Should_Get_flight_by_id_in_db()
        {
            // Arrange
            var flight = dispatcherContext.Flights.FirstOrDefault(f => f.Number == 1111 && f.PointOfDeparture== "TestDeparture1");

            // Act
            var flightResult = _flightService.GetEntity(flight.Id);

            // Assert
            Assert.IsTrue(flightResult != null);
        }

        [Test, Order(4)]
        public void GetEntity_Should_throw_ValidationException_When_flight_with_id_doesnt_exist_in_db()
        {
            // Arrange

            // Assert and Act
            Assert.Throws<ValidationException>(() => _flightService.GetEntity(500000));
        }

        [Test]
        public void UpdateEntity_Should_throw_ValidationException_When_flight_with_id_doesnt_exist_in_db()
        {
            // Arrange
            FlightDTO flightDTO = new FlightDTO
            {
                Number = 4444,
                PointOfDeparture = "Kiyv",
                Destination = "Toronto",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };

            // Assert and Act
            Assert.Throws<ValidationException>(() => _flightService.UpdateEntity(500000, flightDTO));
        }

        [Test]
        public void DeleteEntity_Should_throw_ValidationException_When_flight_with_id_doesnt_exist_in_db()
        {
            // Arrange

            // Assert and Act
            Assert.Throws<ValidationException>(() => _flightService.DeleteEntity(500000));
        }

        [Test]
        public void DeleteEntity_Should_Delete_flight_by_id_from_db()
        {
            // Arrange
            var flight = dispatcherContext.Flights.FirstOrDefault(f => f.Number == 2222 && f.PointOfDeparture== "TestDeparture2");

            // Act
            _flightService.DeleteEntity(flight.Id);

            // Assert
            Assert.Throws<ValidationException>(() => _flightService.GetEntity(flight.Id));
        }

    }
}
