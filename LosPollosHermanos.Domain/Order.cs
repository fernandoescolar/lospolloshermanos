using System;
using System.Collections.Generic;

namespace LosPollosHermanos.Domain
{
    public class Order
    {
        public Order()
        {
            this.OderLines = new List<OrderLine>();
        }

        public int Id { get; set; }

        public DateTime OrderedTimeStamp { get; set; }

        public DateTime? DeliveredTimeStamp { get; set; }

        public List<OrderLine> OderLines { get; set; }

        public bool IsReceivedByStore { get; set; }

        public Guid ClientId { get; set; }
    }
}
