using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface ITransferHeaderRepository : IRepository<TransferHeader, Guid>
    {
    }
}