using System;

namespace BackendAnnonce.Service.Contract
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
