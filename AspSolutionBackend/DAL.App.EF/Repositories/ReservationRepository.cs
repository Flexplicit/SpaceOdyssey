using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.APP.Repositories;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace DAL.App.EF.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation, AppDbContext>, IReservationRepository
    {
        public ReservationRepository(AppDbContext ctx) : base(ctx)
        {
        }


        public override async Task<Reservation?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var res = await CreateQuery(noTracking)
                .Include(reservation => reservation.RouteInfoData)
                .ThenInclude(routeData => routeData.RouteInfo)
                .ThenInclude(routeInfo => routeInfo!.From)
                .Include(reservation => reservation.RouteInfoData)
                .ThenInclude(routeData => routeData.RouteInfo)
                .ThenInclude(routeInfo => routeInfo!.To)
                .Include(reservation => reservation.RouteInfoData)
                .ThenInclude(routeData => routeData.Provider)
                .ThenInclude(provider => provider!.Company)
                .Where(reservation => reservation.Id.Equals(id))
                .FirstOrDefaultAsync();
            AddPriceAndTravelTimeFields(res);

            return res;
        }

        private static void AddPriceAndTravelTimeFields(Reservation? res)
        {
            res!.TotalQuotedPrice = res.RouteInfoData!.Sum(x => x.Provider!.Price);
            res.TotalQuotedTravelTimeInMinutes = res.RouteInfoData!.Sum(route =>
                DateUtils.CalculateHoursBetweenDates(route.Provider!.FlightStart, route.Provider.FlightEnd));
        }


        public override async Task<IEnumerable<Reservation>> GetAllAsync(bool noTracking = true)
        {
            var query = await CreateQuery(noTracking)
                .Include(reservation => reservation.RouteInfoData)
                .ThenInclude(routeData => routeData.RouteInfo)
                .ThenInclude(routeInfo => routeInfo!.From)
                .Include(reservation => reservation.RouteInfoData)
                .ThenInclude(routeData => routeData.RouteInfo)
                .ThenInclude(routeInfo => routeInfo!.To)
                .Include(reservation => reservation.RouteInfoData)
                .ThenInclude(routeData => routeData.Provider)
                .ThenInclude(provider => provider!.Company)
                .ToListAsync();
            query.ForEach(AddPriceAndTravelTimeFields);

            return query;
        }
    }
}