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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayerTests.Services
{
    [TestFixture]
    public class TicketServiceTests
    {
        [Test]
        public void CreateEntity_Should_Create_ticket_typeof_Ticket()
        {
            // Arrange
            TicketDTO ticketDTO = new TicketDTO
            {
                Id = 1,
                FlightId = 1,
                FlightNumber = 1111,
                Price = 120
            };
            Ticket ticket = new Ticket
            {
                Id = 1,
                FlightId = 1,
                FlightNumber = 1111,
                Price = 120
            };

            var ticketRepository = new FakeRepository<Ticket>();
            var ticketService = new TicketService(ticketRepository);

            // Act
            ticketService.CreateEntity(ticketDTO);
            var result = ticketRepository.Get(1);
            // Assert
            Assert.AreEqual(ticket, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_ticket_typeof_Ticket()
        {
            // Arrange
            TicketDTO ticketDTO = new TicketDTO
            {
                Id = 1,
                FlightId = 1,
                FlightNumber = 1111,
                Price = 120
            };
            Ticket ticket = new Ticket
            {
                Id = 1,
                FlightId = 1,
                FlightNumber = 1111,
                Price = 120
            };

            var ticketRepository = A.Fake<IRepository<Ticket>>();
            A.CallTo(() => ticketRepository.Get(A<int>._)).Returns(new Ticket { Id = 1 });

            var ticketService = new TicketService(ticketRepository);

            //Act 
            ticketService.UpdateEntity(1, ticketDTO);
            var result = ticketRepository.Get(1);


            // Assert
            Assert.AreEqual(ticket, result);
        }


        [Test]
        public void UpdateEntity_When_ticket_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            TicketDTO ticketDTO = new TicketDTO
            {
                Id = 1,
                FlightId = 1,
                FlightNumber = 1111,
                Price = 120
            };

            var ticketRepository = A.Fake<IRepository<Ticket>>();
            A.CallTo(() => ticketRepository.Get(A<int>._)).Returns(null);
            var ticketService = new TicketService(ticketRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => ticketService.UpdateEntity(1, ticketDTO));
        }

    }
}
