using System;
using System.Collections.Generic;
using App.Domain.TravelModels;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.APP.Repositories
{
    public interface ICompanyRepository : IBaseRepository<Company, Guid>
    {
        List<Company> GetDistinctCompanies();

    }
}