using Servibes.BusinessProfile.Api.Dto;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DayOfWeek = Servibes.Shared.Enumerations.DayOfWeek;

namespace Servibes.BusinessProfile.Api.Model
{
    public static class WeekHoursRangeFactory
    {
        public static WeekHoursRange Create(List<HoursRangeDto> openHoursDtos)
        {
            var monday = GetHoursRange(openHoursDtos, DayOfWeek.Monday);
            var tuesday = GetHoursRange(openHoursDtos, DayOfWeek.Tuesday);
            var wednesday = GetHoursRange(openHoursDtos, DayOfWeek.Wednesday);
            var thursday = GetHoursRange(openHoursDtos, DayOfWeek.Thursday);
            var friday = GetHoursRange(openHoursDtos, DayOfWeek.Friday);
            var saturday = GetHoursRange(openHoursDtos, DayOfWeek.Saturday);
            var sunday = GetHoursRange(openHoursDtos, DayOfWeek.Sunday);

            return new WeekHoursRange(monday, tuesday, wednesday, thursday, friday, saturday, sunday);
        }

        private static HoursRange GetHoursRange(List<HoursRangeDto> openHoursDtos, DayOfWeek dayOfWeek)
        {
            return openHoursDtos.Where(x => x.DayOfWeek == dayOfWeek)
                .Select(y => new HoursRange(y.IsAvailable, TimeSpan.Parse(y.Start), TimeSpan.Parse(y.End))).FirstOrDefault();
        }
    }
}
