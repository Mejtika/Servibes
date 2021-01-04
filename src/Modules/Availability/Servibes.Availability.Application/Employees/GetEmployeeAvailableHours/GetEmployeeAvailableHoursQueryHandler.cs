using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Servibes.Shared.Database;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.GetEmployeeAvailableHours
{
    public class GetEmployeeAvailableHoursQueryHandler : IRequestHandler<GetEmployeeAvailableHoursQuery, List<AvailableHoursDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly IDateTimeServer _dateTimeServer;

        public GetEmployeeAvailableHoursQueryHandler(
            ISqlConnectionFactory sqlConnection,
            IDateTimeServer dateTimeServer)
        {
            _sqlConnection = sqlConnection;
            _dateTimeServer = dateTimeServer;
        }
        public async Task<List<AvailableHoursDto>> Handle(GetEmployeeAvailableHoursQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnection.GetOpenConnection();
            const string employeeAvailabilitySql = "SELECT" +
                                                   "[EmployeeId], " +
                                                   "[CompanyId] " +
                                                   "FROM [Servibes].[availability].[Employees]" +
                                                   "WHERE [EmployeeId] = @EmployeeId AND [CompanyId] = @CompanyId";
            var employeeAvailability = await connection.QuerySingleOrDefaultAsync<EmployeeAvailabilityDto>(employeeAvailabilitySql, new { request.EmployeeId, request.CompanyId });
            if (employeeAvailability == null)   
            {
                throw new AppException("Employee or company with specified id doesn't exists.");
            }

            const string employeeWorkingHoursSql = "SELECT " +
                                                   "[Start], " +
                                                   "[End], " +
                                                   "[IsAvailable] " +
                                                   "FROM [Servibes].[availability].[WorkingHours]" +
                                                   "WHERE[EmployeeId] = @EmployeeId AND DayOfWeek = @Day";
            var workingHours = await connection.QueryFirstAsync<WorkingHoursDto>(employeeWorkingHoursSql, new { request.EmployeeId, Day = request.Date.DayOfWeek.ToString() });
            if (!workingHours.IsAvailable || (request.Date.Date == _dateTimeServer.Now.Date && _dateTimeServer.Now.TimeOfDay >= workingHours.End))
            {
                return new List<AvailableHoursDto>();
            }

            var nextDay = request.Date.AddDays(1);
            var reservationsForGivenDateSql = "SELECT" +
                                                    "[Start], " +
                                                    "[End] " +
                                                    "FROM [Servibes].[availability].[Reservations] " +
                                                    "WHERE [EmployeeId] = @EmployeeId AND [Start] BETWEEN @DateFrom AND @DateTo";
            var reservations = (await connection.QueryAsync<ReservationDto>(reservationsForGivenDateSql, new { request.EmployeeId, DateFrom = request.Date, DateTo = nextDay })).AsList();
            var availableHours = GetHoursAvailableForReservation(request.Duration, workingHours.Start, workingHours.End, reservations);
            if (request.Date.Date == _dateTimeServer.Now.Date && (_dateTimeServer.Now.TimeOfDay > workingHours.Start && _dateTimeServer.Now.TimeOfDay < workingHours.End))
            {
                var currentTime = _dateTimeServer.Now.TimeOfDay.TotalMinutes;
                var closestAvailableHour = availableHours.Aggregate((x, y) => Math.Abs(x.TotalMinutes - currentTime) < Math.Abs(y.TotalMinutes - currentTime) ? x : y);
                var indexOfClosestHour = availableHours.IndexOf(closestAvailableHour);
                if (currentTime + 5 > closestAvailableHour.TotalMinutes)
                {
                    indexOfClosestHour++;
                }
                availableHours.RemoveRange(0, indexOfClosestHour);
            }
            return availableHours.Select(x => new AvailableHoursDto {Time = x.ToString()}).ToList();
        }

        private List<TimeSpan> GetHoursAvailableForReservation(int serviceTime, TimeSpan start, TimeSpan end, List<ReservationDto> reservations)
        {
            var availableHours = new List<TimeSpan>();
            if (serviceTime == 15)
            {
                availableHours = GetHoursToBookForShortReservation(start, end, reservations);
            }
            else
            {
                availableHours = GetHoursToBookForLongReservation(serviceTime, start, end, reservations);
            }

            return availableHours.Where(x => x.Minutes == 0 || x.Minutes % 15 == 0).ToList();
        }

        private List<TimeSpan> GetHoursToBookForLongReservation(int serviceTime, TimeSpan start, TimeSpan end, List<ReservationDto> reservations)
        {
            var timePeriodsBetween = GetTimePeriodsBetween(start, end);

            foreach (var reservation in reservations)
            {
                var startIndex = timePeriodsBetween.IndexOf(reservation.Start.TimeOfDay.Add(TimeSpan.FromMinutes(5)));
                var endIndex = timePeriodsBetween.IndexOf(reservation.End.TimeOfDay);
                timePeriodsBetween.RemoveRange(startIndex, endIndex - startIndex);
            }

            var hoursToBook = timePeriodsBetween.Where(timePeriod =>
            {
                var checkTime = timePeriod.Add(TimeSpan.FromMinutes(serviceTime));
                var timePeriods = GetTimePeriodsBetween(timePeriod, checkTime);
                return timePeriodsBetween.Contains(checkTime) && ContainsAll(timePeriods);
            }).ToList();

            return hoursToBook;

            bool ContainsAll(List<TimeSpan> timePeriods) => !timePeriods.Except(timePeriodsBetween).Any();
        }

        private List<TimeSpan> GetHoursToBookForShortReservation(TimeSpan start, TimeSpan end, List<ReservationDto> reservations)
        {
            var timePeriodsBetween = GetTimePeriodsBetween(start, end.Add(TimeSpan.FromMinutes(-5)));
            foreach (var reservation in reservations)
            {
                var startIndex = timePeriodsBetween.IndexOf(reservation.Start.TimeOfDay);
                var endIndex = timePeriodsBetween.IndexOf(reservation.End.TimeOfDay);
                if (endIndex == -1)
                {
                    timePeriodsBetween.RemoveAt(timePeriodsBetween.Count - 1);
                    continue;
                }
                timePeriodsBetween.RemoveRange(startIndex, endIndex - startIndex);
            }
            return timePeriodsBetween;
        }

        private List<TimeSpan> GetTimePeriodsBetween(TimeSpan start, TimeSpan end)
        {
            long ticksPerSecond = 10000000;
            long ticksPerMinute = ticksPerSecond * 60;
            long ticksPer5Min = ticksPerMinute * 5;
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            for (long i = start.Ticks; i <= end.Ticks; i += ticksPer5Min)
            {
                timeSpans.Add(TimeSpan.FromTicks(i));
            }
            return timeSpans;
        }
    }
}
