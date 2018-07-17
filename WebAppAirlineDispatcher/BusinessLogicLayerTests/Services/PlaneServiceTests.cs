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
    public class PlaneServiceTests
    {
        [Test]
        public void CreateEntity_Should_Create_plane_typeof_Plane()
        {
            // Arrange
            PlaneDTO planeDTO = new PlaneDTO
            {
                Id = 1,
                ReleaseDate = new DateTime(2018, 07, 12),
                Name="TY-143",
                PlaneTypeId=1,
                Lifetime=new DateTime(2020,07,12)- new DateTime(2018, 07, 12)
            };
            Plane plane = new Plane
            {
                Id = 1,
                ReleaseDate = new DateTime(2018, 07, 12),
                Name = "TY-143",
                PlaneTypeId = 1,
                Lifetime = new DateTime(2020, 07, 12) - new DateTime(2018, 07, 12)
            };

            var planeRepository = new FakeRepository<Plane>();
            var planeService = new PlaneService(planeRepository);

            // Act
            planeService.CreateEntity(planeDTO);
            var result = planeRepository.Get(1);

            // Assert
            Assert.AreEqual(plane, result);
        }

        [Test]
        public void UpdateEntity_Should_Update_plane_typeof_Plane()
        {
            // Arrange
            PlaneDTO planeDTO = new PlaneDTO
            {
                Id = 1,
                ReleaseDate = new DateTime(2018, 07, 12),
                Name = "TY-143",
                PlaneTypeId = 1,
                Lifetime = new DateTime(2020, 07, 12) - new DateTime(2018, 07, 12)
            };
            Plane plane = new Plane
            {
                Id = 1,
                ReleaseDate = new DateTime(2018, 07, 12),
                Name = "TY-143",
                PlaneTypeId = 1,
                Lifetime = new DateTime(2020, 07, 12) - new DateTime(2018, 07, 12)
            };

            var planeRepository = A.Fake<IRepository<Plane>>();
            A.CallTo(() => planeRepository.Get(A<int>._)).Returns(new Plane { Id = 1 });

            var planeService = new PlaneService(planeRepository);

            //Act 
            planeService.UpdateEntity(1, planeDTO);
            var result = planeRepository.Get(1);

            // Assert
            Assert.AreEqual(plane, result);
        }


        [Test]
        public void UpdateEntity_When_plane_doesnt_exist_Then_throw_exception()
        {
            // Arrange
            PlaneDTO planeDTO = new PlaneDTO
            {
                Id = 1,
                ReleaseDate = new DateTime(2018, 07, 12),
                Name = "TY-143",
                PlaneTypeId = 1,
                Lifetime = new DateTime(2020, 07, 12) - new DateTime(2018, 07, 12)
            };

            var planeRepository = A.Fake<IRepository<Plane>>();
            A.CallTo(() => planeRepository.Get(A<int>._)).Returns(null);
            var planeService = new PlaneService(planeRepository);

            //Act and Assert
            Assert.Throws<ValidationException>(() => planeService.UpdateEntity(1, planeDTO));

        }

    }
}
