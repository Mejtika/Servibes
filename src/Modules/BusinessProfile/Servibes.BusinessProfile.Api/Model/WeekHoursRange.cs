namespace Servibes.BusinessProfile.Api.Model
{
    public class WeekHoursRange
    {
        public static WeekHoursRange CreateFromWeekDays(HoursRange monday, HoursRange tuesday, HoursRange wednesday, HoursRange thursday, HoursRange friday, HoursRange saturday, HoursRange sunday)
        {
            return new WeekHoursRange(monday, tuesday, wednesday, thursday, friday, saturday, sunday);
        }

        private WeekHoursRange(
            HoursRange monday,
            HoursRange tuesday,
            HoursRange wednesday,
            HoursRange thursday,
            HoursRange friday,
            HoursRange saturday,
            HoursRange sunday)
        {
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }

        private WeekHoursRange()
        {}

        public HoursRange Monday { get; }
        public HoursRange Tuesday { get; }
        public HoursRange Wednesday { get; }
        public HoursRange Thursday { get; }
        public HoursRange Friday { get; }
        public HoursRange Saturday { get; }
        public HoursRange Sunday { get; }
    }
}