using System;
using System.Collections.Generic;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class ReceiptHeaderDto : FullAuditedEntityDto<Guid>
    {
        public string Code { get; set; }

        public int ReceiptType { get; set; }

        public Guid? PurchaseOrderId { get; set; }

        public DateTime ArriveDatetime { get; set; }

        public DateTime CloseAt { get; set; }

        public int TotalQty { get; set; }

        public int TotalCases { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalVolume { get; set; }

        public string Note { get; set; }

        public Guid WarehouseId { get; set; }

        public ICollection<ReceiptDetailDto> ReceiptDetails { get; set; }

    }
}