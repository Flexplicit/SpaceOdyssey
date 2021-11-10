using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.APP.Repositories;
using DAL.Base.EF.Repositories;
using MoreLinq;

namespace DAL.App.EF.Repositories
{
    public class CompanyRepository : BaseRepository<Company, Guid, AppDbContext>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public List<Company> GetDistinctCompanies()
        {
            return CreateQuery(false)
                .AsEnumerable()
                .DistinctBy(company => company.Name)
                .ToList();
        }
    }
}