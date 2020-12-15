using System;

namespace Servibes.Shared.Services
{
    public class DateTimeServer : IDateTimeServer
    {
        public DateTime Now => DateTime.Now;
    }
}