using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface ICompanies
    {
        PersonalInformation GetCompaniesPersonalInfromation(long companyId);
        long InsertCompany(PersonalInformation personalInformation);
        void UpdateCompanysPersonalInformation(PersonalInformation personalInformation);
    }
}
