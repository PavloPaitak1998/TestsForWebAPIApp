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
    public class CrewServiceTests
    {
        [Test]
        public void CreateEntity_Should_Create_crew_typeof_Crew()
        {
            // Arrange
            CrewDTO crewDTO = new CrewDTO
            {
                Id = 1,
                PilotId=1
            };
            Crew crew = new Crew
            {
                Id = 1,
                PilotId = 1
            };

            var crewRepository = new FakeRepository<Crew>();
            var crewService = new CrewService(crewRepository);

            // Act
            crewService.CreateEntity(crewDTO);
            var result = crewRepository.Get(1);

            // Assert
            Assert.AreEqual(crew, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_crew_typeof_Crew()
        {
            // Arrange
            CrewDTO crewDTO = new CrewDTO
            {
                Id = 1,
                PilotId = 1
            };
            Crew crew = new Crew
            {
                Id = 1,
                PilotId = 1
            };
            var crewRepository = A.Fake<IRepository<Crew>>();
            A.CallTo(() => crewRepository.Get(A<int>._)).Returns(new Crew { Id = 1 });

            var crewService = new CrewService(crewRepository);

            //Act 
            crewService.UpdateEntity(1, crewDTO);
            var result = crewRepository.Get(1);


            // Assert
            Assert.AreEqual(crew, result);
        }


        [Test]
        public void UpdateEntity_When_crew_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            CrewDTO crewDTO = new CrewDTO
            {
                Id = 1,
                PilotId = 1
            };

            var crewRepository = A.Fake<IRepository<Crew>>();
            A.CallTo(() => crewRepository.Get(A<int>._)).Returns(null);
            var crewService = new CrewService(crewRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => crewService.UpdateEntity(1, crewDTO));

        }

    }
}
