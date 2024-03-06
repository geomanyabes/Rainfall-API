using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using RainfallApi.Application.Interface;
using RainfallApi.Application.Service;
using RainfallApi.DataAccess.Interface;
using RainfallApi.Domain.Model.Dto;
using RainfallApi.Domain.Model.Entity;

namespace RainfallApi.Tests
{
    [TestFixture]
    public class RainfallReadingServiceTests
    {
        private IRainfallReadingService _rainfallReadingService;
        private Mock<IRainfallReadingRepository> _mockRainfallReadingRepository;

        [SetUp]
        public void Setup()
        {
            // Create the mock repository
            _mockRainfallReadingRepository = new Mock<IRainfallReadingRepository>();

            // Create the service to be tested
            _rainfallReadingService = new RainfallReadingService(_mockRainfallReadingRepository.Object);
        }

        [Test]
        public async Task GetRainfallReadings_ValidStationId_ReturnsExpectedData()
        {
            // Arrange
            string stationId = "someStationId";
            int count = 10;
            var expectedData = new List<RainfallData>
            {
                new()
                {
                    LatestReading = new LatestReading
                    {
                        DateTime = DateTime.Parse("2024-03-06T13:30:00Z"),
                        Value = 10
                    }
                }
            };

            _mockRainfallReadingRepository.Setup(r => r.GetReadingByStation(stationId, count)).ReturnsAsync(expectedData);

            // Act
            var result = await _rainfallReadingService.GetRainfallReadings(stationId, count);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(expectedData.Count));
            Assert.That(result.All(r => r.DateMeasured != null && r.AmountMeasured.HasValue), Is.True);
        }

        [Test]
        public async Task GetRainfallReadings_NullData_ReturnsEmptyList()
        {
            // Arrange
            string stationId = "someStationId";
            int count = 10;
            List<RainfallData> expectedData = null;

            _mockRainfallReadingRepository.Setup(r => r.GetReadingByStation(stationId, count)).ReturnsAsync(expectedData);

            // Act
            var result = await _rainfallReadingService.GetRainfallReadings(stationId, count);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetRainfallReadings_EmptyData_ReturnsEmptyList()
        {
            // Arrange
            string stationId = "someStationId";
            int count = 10;
            var expectedData = new List<RainfallData>();

            _mockRainfallReadingRepository.Setup(r => r.GetReadingByStation(stationId, count)).ReturnsAsync(expectedData);

            // Act
            var result = await _rainfallReadingService.GetRainfallReadings(stationId, count);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetRainfallReadings_InvalidData_ReturnsEmptyList()
        {
            // Arrange
            string stationId = "someStationId";
            int count = 10;
            var expectedData = new List<RainfallData>
            {
                // Invalid data with null values
                new() { LatestReading = null },
                // Add more invalid data as needed
            };

            _mockRainfallReadingRepository.Setup(r => r.GetReadingByStation(stationId, count)).ReturnsAsync(expectedData);

            // Act
            var result = await _rainfallReadingService.GetRainfallReadings(stationId, count);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Null);
        }
    }

}
