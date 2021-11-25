using BackendAnnonce.Service.Contract;
using System;

namespace BackendAnnonce.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}