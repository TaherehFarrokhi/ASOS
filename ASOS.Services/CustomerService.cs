using System;

namespace ASOS.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyCreditLimitCalculator _companyCreditLimitCalculator;

        public CustomerService(ICustomerRepository customerRepository, ICompanyCreditLimitCalculator companyCreditLimitCalculator)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _companyCreditLimitCalculator = companyCreditLimitCalculator ?? throw new ArgumentNullException(nameof(companyCreditLimitCalculator));
        }

        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            var customer = new Customer
            {
                Firstname = firname,
                Surname = surname,
                Email = email,
                CompanyId = companyId,
                DateOfBirth = dateOfBirth
            };

            var customerCreditLimit = _companyCreditLimitCalculator.CalculateAsync(companyId).Result;
            if (customerCreditLimit < 500)
                return false;

            customer.CreditLimit = customerCreditLimit;

            _customerRepository.SaveCustomer(customer);

            return true;
        }
    }
}
