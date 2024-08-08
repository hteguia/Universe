using Domain.Features.SmsMarketings.Models;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.SmsMarketings.Repositories;

public interface ISmsRepository : IAsyncRepository<Sms>
{
    
}