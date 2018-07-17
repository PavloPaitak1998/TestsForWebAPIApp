using BusinessLogicLayer.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppAirlineDispatcher.Controllers;

namespace BusinessLogicLayerTests.Controllers
{
    [TestFixture]
    public class FlightsControllerTests
    {
        [Test]
        public void Get_Should_get_all_flights()
        {
            // Arrange
            var flight1 = new FlightDTO {
                Number = 1111,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "London",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };
            var flight2 = new FlightDTO {
                Number = 2222,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 11, 18, 23, 0),
                Destination = "Tokio", DestinationTime = new DateTime(2018, 07, 12, 18, 0, 0)
            };

            var flightService = A.Fake<IFlightService>();
            A.CallTo(() => flightService.GetEntities()).Returns(new List<FlightDTO> { flight1, flight2 });

           var flightsController = new FlightsController(flightService);

            //Act
            var actionResult = flightsController.Get();

            //Assert
            Assert.NotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;

            Assert.NotNull(result);

            List<FlightDTO> flights = result.Value as List<FlightDTO>;

            Assert.AreEqual(2, flights.Count());
            Assert.AreEqual(flight1, flights[0]);
            Assert.AreEqual(flight2, flights[1]);
        }

        [Test]
        public void Get_Should_get_flight_by_id()
        {
            // Arrange
            var flightDTO = new FlightDTO
            {
                Id=1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "London",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };


            var flightService = A.Fake<IFlightService>();
            A.CallTo(() => flightService.GetEntity(A<int>._)).Returns( flightDTO );

            var flightsController = new FlightsController(flightService);

            //Act
            var actionResult = flightsController.Get(1);

            //Assert
            Assert.NotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;

            Assert.NotNull(result);

            FlightDTO flightDTOResult = result.Value as FlightDTO;

            Assert.AreEqual(flightDTO, flightDTOResult);
        }

        [Test]
        public void Post_Should_create_new_flight_return_statusCode_200()
        {
            // Arrange
            var flightDTO = new FlightDTO
            {
                Number = 1111,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "London",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };

            var flightService = A.Fake<IFlightService>();

            var flightsController = new FlightsController(flightService);

            //Act
            var actionResult = flightsController.Post(flightDTO);

            //Assert
            Assert.NotNull(actionResult);
            OkObjectResult result = actionResult as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Put_Should_change_some_value_in_existing_flight_return_statusCode_200()
        {
            var flightDTO1 = new FlightDTO
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "London",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };
            var flightDTO2 = new FlightDTO
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Kiyv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "Paris",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };

            var flightService = A.Fake<IFlightService>();
            A.CallTo(() => flightService.GetEntity(A<int>._)).Returns(flightDTO1);

            var flightsController = new FlightsController(flightService);

            //Act
            var actionResult = flightsController.Put(1, flightDTO2);

            //Assert
            Assert.NotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Delete_Should_delete_flight_by_id_return_statusCode_204()
        {
            // Arrange
            var flightDTO = new FlightDTO
            {
                Id = 1,
                Number = 1111,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "London",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };

            var flightService = A.Fake<IFlightService>();
            A.CallTo(() => flightService.GetEntity(A<int>._)).Returns(flightDTO);

            var flightsController = new FlightsController(flightService);

            //Act
            var actionResult = flightsController.Delete(1);

            //Assert
            Assert.NotNull(actionResult);

            NoContentResult result = actionResult as NoContentResult;

            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void Delete_Should_delete_all_flights_return_statusCode_204()
        {
            // Arrange
            var flight1 = new FlightDTO
            {
                Number = 1111,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 10, 18, 23, 0),
                Destination = "London",
                DestinationTime = new DateTime(2018, 07, 11, 18, 23, 0)
            };
            var flight2 = new FlightDTO
            {
                Number = 2222,
                PointOfDeparture = "Lviv",
                DepartureTime = new DateTime(2018, 07, 11, 18, 23, 0),
                Destination = "Tokio",
                DestinationTime = new DateTime(2018, 07, 12, 18, 0, 0)
            };

            var flightService = A.Fake<IFlightService>();
            A.CallTo(() => flightService.GetEntities()).Returns(new List<FlightDTO> { flight1, flight2 });

            var flightsController = new FlightsController(flightService);

            //Act
            var actionResult = flightsController.Delete();

            //Assert
            Assert.NotNull(actionResult);

            NoContentResult result = actionResult as NoContentResult;

            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

    }
}
