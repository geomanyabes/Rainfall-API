using Moq;
using Moq.Protected;
using RainfallApi.DataAccess.Repository;
using System.Net;
using System.Text;

namespace RainfallApi.Tests.DataAccess.Repository
{
    [TestFixture]
    public class RainfallReadingRepositoryTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _mockHttpClient;
        private RainfallReadingRepository _repository;

        [SetUp]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _repository = new RainfallReadingRepository(_mockHttpClient);
        }

        //[Test]
        //public async Task GetReadingByStation_ValidData_ReturnsData()
        //{
        //    // Arrange
        //    var expectedStationId = "someStationId";
        //    var expectedLimit = 10;
        //    var expectedResponseContent = MockExternalRainfallApiResponse.GetValidResponseContent();
        //    var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(expectedResponseContent)
        //    };

        //    // Set up the mock message handler to return the expected response
        //    var mockHttpMessageHandler = _mockHttpClient.();
        //    mockHttpMessageHandler
        //        .When("https://environment.data.gov.uk/flood-monitoring/id/stations/someStationId/measures?_limit=10")
        //        .Respond(HttpStatusCode.OK, expectedResponseContent);

        //    // Act
        //    var result = await _repository.GetReadingByStation(expectedStationId, expectedLimit);

        //    // Assert
        //    Assert.That(result, Is.Not.Null);
        //    // Perform additional assertions as needed
        //}

        [Test]
        public async Task GetReadingByStation_ValidData_ReturnsData()
        {
            // Arrange
            // Arrange
            var expectedStationId = "3680";
            var expectedLimit = 1;
            var expectedResponseContent = GetValidResponseContent();

            // Set up the mock HttpClient to return the expected response content
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponseContent, Encoding.UTF8, "application/json")
                });

            // Act
            var result = await _repository.GetReadingByStation(expectedStationId, expectedLimit);

            // Assert
            Assert.That(result, Is.Not.Null); // Ensure the result is not null
            Assert.That(result.Count, Is.EqualTo(1)); // Ensure there is exactly one item in the result list

            // Assert properties of the first item in the result list
            var expectedDateTime = new DateTime(2024, 03, 06, 14, 15, 00, DateTimeKind.Utc);
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Label, Is.EqualTo("rainfall-tipping_bucket_raingauge-t-15_min-mm")); // Check the Label property
                Assert.That(result[0].LatestReading, Is.Not.Null); // Ensure LatestReading is not null
                Assert.That(result[0].LatestReading.DateTime, Is.EqualTo(expectedDateTime)); // Check the DateTime property
                Assert.That(result[0].LatestReading.Value, Is.EqualTo(0.0m)); // Check the Value property
            });
        }

        [Test]
        public async Task GetReadingByStation_InvalidStationId_ReturnsNull()
        {
            // Arrange
            var invalidStationId = "nonexistentId";
            var limit = 10;

            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound, // Simulate a 404 Not Found response
                    Content = new StringContent(""),
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);
            var repository = new RainfallReadingRepository(httpClient);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await repository.GetReadingByStation(invalidStationId, limit));
            Assert.That(exception.Message, Is.EqualTo($"Failed to retrieve stations. Status code: NotFound"));
        }


        [Test]
        public void GetReadingByStation_InvalidHttpClient_ThrowsException()
        {
            // Arrange
            HttpClient invalidHttpClient = null; // Invalid HttpClient instance

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RainfallReadingRepository(invalidHttpClient));
        }

        private static string GetValidResponseContent()
        {
            return @"{ 
                ""@context"" : ""http://environment.data.gov.uk/flood-monitoring/meta/context.jsonld"" ,
                ""meta"" : { 
                    ""publisher"" : ""Environment Agency"" ,
                    ""licence"" : ""http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/"" ,
                    ""documentation"" : ""http://environment.data.gov.uk/flood-monitoring/doc/reference"" ,
                    ""version"" : ""0.9"" ,
                    ""comment"" : ""Status: Beta service"" ,
                    ""hasFormat"" : [ ""http://environment.data.gov.uk/flood-monitoring/id/stations/3680/measures.csv?_limit=1"", ""http://environment.data.gov.uk/flood-monitoring/id/stations/3680/measures.rdf?_limit=1"", ""http://environment.data.gov.uk/flood-monitoring/id/stations/3680/measures.ttl?_limit=1"", ""http://environment.data.gov.uk/flood-monitoring/id/stations/3680/measures.html?_limit=1"" ] ,
                    ""limit"" : 1
                }
                ,
                ""items"" : [ { 
                    ""@id"" : ""http://environment.data.gov.uk/flood-monitoring/id/measures/3680-rainfall-tipping_bucket_raingauge-t-15_min-mm"" ,
                    ""label"" : ""rainfall-tipping_bucket_raingauge-t-15_min-mm"" ,
                    ""latestReading"" : { 
                        ""@id"" : ""http://environment.data.gov.uk/flood-monitoring/data/readings/3680-rainfall-tipping_bucket_raingauge-t-15_min-mm/2024-03-06T14-15-00Z"" ,
                        ""date"" : ""2024-03-06"" ,
                        ""dateTime"" : ""2024-03-06T14:15:00Z"" ,
                        ""measure"" : ""http://environment.data.gov.uk/flood-monitoring/id/measures/3680-rainfall-tipping_bucket_raingauge-t-15_min-mm"" ,
                        ""value"" : 0.0
                    }
                    ,
                    ""notation"" : ""3680-rainfall-tipping_bucket_raingauge-t-15_min-mm"" ,
                    ""parameter"" : ""rainfall"" ,
                    ""parameterName"" : ""Rainfall"" ,
                    ""period"" : 900 ,
                    ""qualifier"" : ""Tipping Bucket Raingauge"" ,
                    ""station"" : ""http://environment.data.gov.uk/flood-monitoring/id/stations/3680"" ,
                    ""stationReference"" : ""3680"" ,
                    ""unit"" : ""http://qudt.org/1.1/vocab/unit#Millimeter"" ,
                    ""unitName"" : ""mm"" ,
                    ""valueType"" : ""total""
                }
                ]
            }";
        }
    }
}
