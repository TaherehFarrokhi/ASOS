using System.ServiceModel;

namespace CompanyWcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICompanyLimitService" in both code and config file together.
    [ServiceContract]
    public interface ICompanyLimitService
    {
        [OperationContract]
        long GetLimit(int companyId);
    }
}
