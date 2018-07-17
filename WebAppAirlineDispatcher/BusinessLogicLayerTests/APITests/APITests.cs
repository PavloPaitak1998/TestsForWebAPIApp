using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using Shared.DTO;
using WebAppAirlineDispatcher;

namespace BusinessLogicLayerTests.APITests
{
    [TestFixture]
    public class APITests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public APITests()
        {
            _server = new TestServer(new WebHostBuilder()
                            .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task Flights_Should_Get_All()
        {
            // Act
            var response = await _client.GetAsync("/api/flights");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var flights = JsonConvert.DeserializeObject<List<FlightDTO>>(responseString);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(flights.Count() > 0);
        }

        [Test]
        public async Task Flights_Should_Get_Specific()
        {
            // Arrange
            var responseForArrange = await _client.GetAsync("/api/flights");
            responseForArrange.EnsureSuccessStatusCode();
            var responseStringForArrange = await responseForArrange.Content.ReadAsStringAsync();
            var flights = JsonConvert.DeserializeObject<List<FlightDTO>>(responseStringForArrange);

            // Act
            var response = await _client.GetAsync($"/api/flights/{flights[flights.Count()-1].Id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var flight = JsonConvert.DeserializeObject<FlightDTO>(responseString);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(flights[flights.Count() - 1].Id, flight.Id);
        }

        [Test,Order(1)]
        public async Task Flights_Should_Post_Specific()
        {
            // Arrange
            var flightToAdd = new FlightDTO
            {
                Number = 9999,
                PointOfDeparture = "Lviv",
                Destination = "London",
                DepartureTime = new DateTime(2018, 07, 12),
                DestinationTime = new DateTime(2018, 07, 12)
            };
            var content = JsonConvert.SerializeObject(flightToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            // Act
            var response = await _client.PostAsync("/api/flights", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var flight = JsonConvert.DeserializeObject<FlightDTO>(responseString);

            Assert.AreEqual(flightToAdd.Number, flight.Number);
        }

        [Test]
        public async Task Flights_Post_Specific_Invalid()
        {
            // Arrange
            var flightToAdd = new FlightDTO { Number=1010 };
            var content = JsonConvert.SerializeObject(flightToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/flights", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Flights_Put_Specific()
        {
            // Arrange
            var responseForArrange = await _client.GetAsync("/api/flights");
            responseForArrange.EnsureSuccessStatusCode();
            var responseStringForArrange = await responseForArrange.Content.ReadAsStringAsync();
            var flights = JsonConvert.DeserializeObject<List<FlightDTO>>(responseStringForArrange);

            var flightToChange = new FlightDTO
            {
                Destination = "Moldova"
            };
            var content = JsonConvert.SerializeObject(flightToChange);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/flights/{flights[flights.Count()-1].Id}", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var flight = JsonConvert.DeserializeObject<FlightDTO>(responseString);

            Assert.AreEqual(flightToChange.Destination, flight.Destination);
        }

        [Test]
        public async Task Flights_Put_Specific_Invalid()
        {
            // Arrange
            var flightToChange = new FlightDTO { Destination = "Moldova" };
            var content = JsonConvert.SerializeObject(flightToChange);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/flights/10000", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Test,Order(7)]
        public async Task Flights_Delete_Specific()
        {
            // Arrange
            var responseGet = await _client.GetAsync("/api/flights");
            responseGet.EnsureSuccessStatusCode();
            var responseString = await responseGet.Content.ReadAsStringAsync();
            var flights = JsonConvert.DeserializeObject<List<FlightDTO>>(responseString);

            // Act
            var response = await _client.DeleteAsync($"/api/flights/{flights[flights.Count()-1].Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
