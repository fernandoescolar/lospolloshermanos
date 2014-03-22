using System.Runtime.Serialization;

namespace LosPollosHermanos.ServiceContracts
{
    [DataContract]
    public class OrderRequestLine
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}