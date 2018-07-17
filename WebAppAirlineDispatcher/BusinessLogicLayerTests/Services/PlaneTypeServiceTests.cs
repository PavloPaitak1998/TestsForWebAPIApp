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
    public class PlaneTypeServiceTests
    {
        [Test]
        public void CreateEntity_Should_Create_planeType_typeof_PlaneType()
        {
            // Arrange
            PlaneTypeDTO planeTypeDTO = new PlaneTypeDTO
            {
                Id = 1,
                Carrying = 240000,
                Model = "Passenger's",
                Seats = 200
            };
            PlaneType planeType = new PlaneType
            {
                Id = 1,
                Carrying = 240000,
                Model = "Passenger's",
                Seats = 200
            };

            var planeTypeRepository = new FakeRepository<PlaneType>();
            var planeTypeService = new PlaneTypeService(planeTypeRepository);

            // Act
            planeTypeService.CreateEntity(planeTypeDTO);
            var result = planeTypeRepository.Get(1);

            // Assert
            Assert.AreEqual(planeType, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_planeType_typeof_PlaneType()
        {
            // Arrange
            PlaneTypeDTO planeTypeDTO = new PlaneTypeDTO
            {
                Id = 1,
                Carrying = 240000,
                Model = "Passenger's",
                Seats = 200
            };
            PlaneType planeType = new PlaneType
            {
                Id = 1,
                Carrying = 240000,
                Model = "Passenger's",
                Seats = 200
            };

            var planeTypeRepository = A.Fake<IRepository<PlaneType>>();
            A.CallTo(() => planeTypeRepository.Get(A<int>._)).Returns(new PlaneType { Id = 1 });

            var planeTypeService = new PlaneTypeService(planeTypeRepository);

            //Act 
            planeTypeService.UpdateEntity(1, planeTypeDTO);
            var result = planeTypeRepository.Get(1);


            // Assert
            Assert.AreEqual(planeType, result);
        }


        [Test]
        public void UpdateEntity_When_planeType_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            PlaneTypeDTO planeTypeDTO = new PlaneTypeDTO
            {
                Id = 1,
                Carrying = 240000,
                Model = "Passenger's",
                Seats = 200
            };

            var planeTypeRepository = A.Fake<IRepository<PlaneType>>();
            A.CallTo(() => planeTypeRepository.Get(A<int>._)).Returns(null);
            var planeTypeService = new PlaneTypeService(planeTypeRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => planeTypeService.UpdateEntity(1, planeTypeDTO));

        }

    }
}
