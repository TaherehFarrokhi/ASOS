using System;

namespace ASOS.Services
{
    public class Customer
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CompanyId { get; set; }
        public long? CreditLimit { get; set; }
    }
}