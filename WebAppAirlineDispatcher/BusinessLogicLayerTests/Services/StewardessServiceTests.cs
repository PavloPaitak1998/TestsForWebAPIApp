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
    public class StewardessServiceTests
    {
        [Test]
        public void CreateEntity_Should_Create_stewardess_typeof_Stewardess()
        {
            // Arrange
            StewardessDTO stewardessDTO = new StewardessDTO
            {
                Id = 1,
                FirstName = "Anna",
                LastName = "Gohon",
                CrewId = 1,
                BirthDate = new DateTime(1998, 07, 12)
            };
            Stewardess stewardess = new Stewardess
            {
                Id = 1,
                FirstName = "Anna",
                LastName = "Gohon",
                CrewId = 1,
                BirthDate = new DateTime(1998, 07, 12)
            };

            var stewardessRepository = new FakeRepository<Stewardess>();
            var stewardessService = new StewardessService(stewardessRepository);

            // Act
            stewardessService.CreateEntity(stewardessDTO);
            var result = stewardessRepository.Get(1);

            // Assert
            Assert.AreEqual(stewardess, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_stewardess_typeof_Stewardess()
        {
            // Arrange
            StewardessDTO stewardessDTO = new StewardessDTO
            {
                Id = 1,
                FirstName = "Anna",
                LastName = "Gohon",
                CrewId = 1,
                BirthDate = new DateTime(1998, 07, 12)
            };
            Stewardess stewardess = new Stewardess
            {
                Id = 1,
                FirstName = "Anna",
                LastName = "Gohon",
                CrewId = 1,
                BirthDate = new DateTime(1998, 07, 12)
            };

            var stewardessRepository = A.Fake<IRepository<Stewardess>>();
            A.CallTo(() => stewardessRepository.Get(A<int>._)).Returns(new Stewardess { Id = 1 });

            var stewardessService = new StewardessService(stewardessRepository);

            //Act 
            stewardessService.UpdateEntity(1, stewardessDTO);
            var result = stewardessRepository.Get(1);


            // Assert
            Assert.AreEqual(stewardess, result);
        }


        [Test]
        public void UpdateEntity_When_stewardess_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            StewardessDTO stewardessDTO = new StewardessDTO
            {
                Id = 1,
                FirstName = "Anna",
                LastName = "Gohon",
                CrewId = 1,
                BirthDate = new DateTime(1998, 07, 12)
            };

            var stewardessRepository = A.Fake<IRepository<Stewardess>>();
            A.CallTo(() => stewardessRepository.Get(A<int>._)).Returns(null);
            var stewardessService = new StewardessService(stewardessRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => stewardessService.UpdateEntity(1, stewardessDTO));

        }

    }
}
