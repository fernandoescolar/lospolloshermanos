using System.Runtime.Serialization;

namespace LosPollosHermanos.ServiceContracts
{
    [DataContract]
    public class UpdateOrderRequest
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public OrderStatus Status { get; set; }
    }
}