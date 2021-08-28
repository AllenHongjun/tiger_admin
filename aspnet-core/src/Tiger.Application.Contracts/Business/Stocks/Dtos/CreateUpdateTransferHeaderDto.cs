using System;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class CreateUpdateTransferHeaderDto
    {
        public string Code { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CloasAt { get; set; }

        public string CloseBy { get; set; }

        public string Note { get; set; }

        public int TotalQty { get; set; }

        public int TotalWeight { get; set; }

        public int TotalVolume { get; set; }

    }
}