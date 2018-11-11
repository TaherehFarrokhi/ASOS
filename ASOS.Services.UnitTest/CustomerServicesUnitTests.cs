using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ASOS.Services.UnitTest
{
    [TestFixture]
    public class CustomerServicesUnitTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<ICompanyCreditLimitCalculator> _companyCreditLimitCalculatorMock;
        private CustomerService _subject;

        [SetUp]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _companyCreditLimitCalculatorMock = new Mock<ICompanyCreditLimitCalculator>();
            _subject = new CustomerService(_customerRepositoryMock.Object, _companyCreditLimitCalculatorMock.Object);
        }

        [Test]
        public void Should_AddCustomer_ReturnFalse_WhenTheCompanyCreditLimitIsLessThan500()
        {
            // Arrange
            SetupCreditLimit(100);

            // Act
            var result = _subject.AddCustomer("FName", "sName", "test@hotmail.com", DateTime.Now.AddYears(-20), 1);

            // Assert
            result.Should().BeFalse();
            _customerRepositoryMock.Verify(x => x.SaveCustomer(It.IsAny<Customer>()), Times.Never);
        }

        [Test]
        public void Should_AddCustomer_CreateACustomerWithNoLimit_WhenTheCompanyIsVeryImportantClient()
        {
            // Arrange
            SetupCreditLimit(null);

            // Act
            var result = _subject.AddCustomer("FName", "sName", "test@hotmail.com", DateTime.Now.AddYears(-20), 1);

            // Assert
            result.Should().BeTrue();
            _customerRepositoryMock.Verify(x => x.SaveCustomer(It.Is<Customer>(c => c.CreditLimit == null)), Times.Once);
        }

        [Test]
        public void Should_AddCustomer_CreateACustomerDoubleTheLimit_WhenTheCompanyIsImportantClient()
        {
            // Arrange
            SetupCreditLimit(1000);

            // Act
            var result = _subject.AddCustomer("FName", "sName", "test@hotmail.com", DateTime.Now.AddYears(-20), 1);

            // Assert
            result.Should().BeTrue();
            _customerRepositoryMock.Verify(x => x.SaveCustomer(It.Is<Customer>(c => c.CreditLimit == 1000)), Times.Once);
        }

        private void SetupCreditLimit(long? limit)
        {
            _companyCreditLimitCalculatorMock.Setup(x => x.CalculateAsync(It.Is<int>(c => c == 1))).ReturnsAsync(limit);
        }
    }
}
