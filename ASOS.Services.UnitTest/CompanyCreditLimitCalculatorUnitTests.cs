using System.Threading.Tasks;
using FluentAssertions;
using Limits;
using Moq;
using NUnit.Framework;

namespace ASOS.Services.UnitTest
{
    [TestFixture]
    public class CompanyCreditLimitCalculatorUnitTests
    {
        private Mock<ICompanyRepository> _companyRepositoryMock;
        private Mock<ICompanyLimitService> _companyLimitServiceMock;

        private CompanyCreditLimitCalculator _subject;

        [SetUp]
        public void Setup()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _companyLimitServiceMock = new Mock<ICompanyLimitService>();
            _subject = new CompanyCreditLimitCalculator(_companyRepositoryMock.Object, _companyLimitServiceMock.Object);
        }

        [Test]
        public async Task Calculate_Should_ReturnsNull_WhenCompanyIsVeryImportant()
        {
            // Arrange
            _companyRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Company { CompanyType = CompanyType.VeryImportantClient });

            // Act
            var creditLimit = await _subject.CalculateAsync(1);
            
            //Assert
            creditLimit.Should().BeNull();
        }

        [TestCase(500)]
        public async Task Calculate_Should_ReturnsDoubleOfCompanyCreditLimit_WhenCompanyIsImportant(long companyCreditLimit)
        {
            // Arrange
            SetupServices(companyCreditLimit, CompanyType.ImportantClient);
            // Act
            var creditLimit = await _subject.CalculateAsync(1);

            //Assert
            creditLimit.Should().Be(companyCreditLimit * 2);
        }


        [TestCase(500)]
        public async Task Calculate_Should_ReturnsCompanyCreditLimit_WhenCompanyIsNotImportantNorVeryImportant(long companyCreditLimit)
        {
            // Arrange
            SetupServices(companyCreditLimit, CompanyType.Unknown);
            // Act
            var creditLimit = await _subject.CalculateAsync(1);

            //Assert
            creditLimit.Should().Be(companyCreditLimit);
        }

        private void SetupServices(long companyCreditLimit, CompanyType companyType)
        {
            _companyLimitServiceMock.Setup(x => x.GetLimitAsync(It.IsAny<int>())).ReturnsAsync(companyCreditLimit);
            _companyRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Company { CompanyType = companyType});
        }
    }
}