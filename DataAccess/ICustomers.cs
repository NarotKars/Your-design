using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface ICustomers
    {
        PersonalInformation GetCustomersPersonalInfromation(long userId);
        long InsertCustomer(PersonalInformation personalInformation);
        void UpdateCustomersPersonalInformation(PersonalInformation personalInformation);
    }
}
