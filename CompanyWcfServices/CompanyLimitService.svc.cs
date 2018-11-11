namespace CompanyWcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CompanyLimitService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CompanyLimitService.svc or CompanyLimitService.svc.cs at the Solution Explorer and start debugging.
    public class CompanyLimitService : ICompanyLimitService
    {

        public long GetLimit(int companyId)
        {
            return 100;
        }
    }
}
