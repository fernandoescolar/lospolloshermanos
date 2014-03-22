using System;
using System.Runtime.Serialization;

namespace LosPollosHermanos.ServiceContracts
{
    [DataContract]
    public class OrderRequest
    {
        [DataMember]
        public Guid Client { get; set; }
        
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public OrderRequestLine[] Lines { get; set; }
    }
}