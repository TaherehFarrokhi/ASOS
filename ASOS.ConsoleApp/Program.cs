using System;
using System.ServiceModel;
using Limits;

namespace ASOS.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();

            var service = new CompanyLimitServiceClient();
            service.Endpoint.Address = new EndpointAddress("http://localhost:62863/CompanyLimitService.svc");

            var result = service.GetLimitAsync(1).Result;

            Console.WriteLine($"Result is {result}");

            Console.ReadKey();

        }
    }
}
