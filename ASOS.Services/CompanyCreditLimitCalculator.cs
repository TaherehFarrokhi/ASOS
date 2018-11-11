using System;
using System.Threading.Tasks;
using Limits;

namespace ASOS.Services
{
    public class CompanyCreditLimitCalculator : ICompanyCreditLimitCalculator
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyLimitService _companyLimitService;

        public CompanyCreditLimitCalculator(ICompanyRepository companyRepository, ICompanyLimitService companyLimitService)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _companyLimitService = companyLimitService ?? throw new ArgumentNullException(nameof(companyLimitService));
        }

        public async Task<long?> CalculateAsync(int companyId)
        {
            var company = _companyRepository.Get(companyId);

            if (company.CompanyType == CompanyType.VeryImportantClient)
                return null;

            var creditLimit = await _companyLimitService.GetLimitAsync(companyId);

            if (company.CompanyType == CompanyType.ImportantClient)
                return creditLimit * 2;

            return creditLimit;
        }
    }
}