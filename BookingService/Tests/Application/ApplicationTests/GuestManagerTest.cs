using Application.Guest;
using Application.Guest.Dto;
using Application.Guest.Requests;
using Application.Responses;
using Domain.DomainEntities;
using Domain.DomainPorts;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace ApplicationTests
{

    public class GuestManagerTest
    {
        GuestManager? guestManager;

        [Fact]
        public async Task HappyPath()
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = "a@a.com",
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            int expectedId = 222;

            var request = new CreateGuestRequest
            {
                Data = guestDto
            };

            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(expectedId, res.Data.Id);
            Assert.Equal(guestDto.Name, res.Data.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = "a@a.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest
            {
                Data = guestDto
            };

            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCode.INVALID_DOCUMENT, res.ErrorCode);
            Assert.Equal("Invalid ID passed", res.Message);
        }

        [Theory]
        [InlineData(null, "Ciclano", "a@a.com")]
        [InlineData("", "Ciclano", "a@a.com")]
        [InlineData("Fulano", null, "a@a.com")]
        [InlineData("Fulano", "", "a@a.com")]
        [InlineData("Fulano", "Ciclano", null)]
        [InlineData("Fulano", "Ciclano", "")]
        [InlineData("", "", "")]
        [InlineData("", "", null)]
        [InlineData("", null, "")]
        [InlineData(null, "", "")]
        [InlineData(null, null, null)]
        public async Task Should_Return_MissingRequiredInformationException_WhenDocsAreInvalid(
            string name,
            string surname,
            string email)
        {
            var guestDto = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest
            {
                Data = guestDto
            };

            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCode.MISSING_REQUIRED_INFORMATION, res.ErrorCode);
            Assert.Equal("Missing Required Information", res.Message);
        }

        [Theory]
        [InlineData("b@b.com")]
        public async Task Should_Return_InvalidEmailExceptionException_WhenDocsAreInvalid(string email)
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest
            {
                Data = guestDto
            };

            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCode.INVALID_EMAIL, res.ErrorCode);
            Assert.Equal("Invalid Email", res.Message);
        }

        [Fact]
        public async Task Should_Return_GuestNotFound_When_GuestDoesentExist()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.GetGuestAsync(333);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCode.NOT_FOUND, res.ErrorCode);
            Assert.Equal("No Guest record was found with the given Id", res.Message);
        }

        [Fact]
        public async Task Should_Return_Guest_Success()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Teste",
                DocumentId = new Domain.DomainValueObjects.PersonId
                {
                    DocumentType = Domain.DomainEnums.DocumentType.DriverLicense,
                    IdNumber = "123"
                }
            };

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Guest?)fakeGuest));

            guestManager = new GuestManager(fakeRepo.Object);

            var res  = await guestManager.GetGuestAsync(333);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(fakeGuest.Id, res.Data.Id);
            Assert.Equal(fakeGuest.Name, res.Data.Name);
        }
    }
}