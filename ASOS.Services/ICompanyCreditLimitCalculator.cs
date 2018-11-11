using System;
using System.Threading.Tasks;

namespace ASOS.Services
{
    public interface ICompanyCreditLimitCalculator
    {
        Task<long?> CalculateAsync(int companyId);
    }

    public interface ICompanyRepository
    {
        Company Get(int companyId);
    }

    public class Company
    {
        public int Id { get; set; }
        public CompanyType CompanyType { get; set; }
    }

    public enum CompanyType
    {
        Unknown,
        VeryImportantClient,
        ImportantClient

    }
}