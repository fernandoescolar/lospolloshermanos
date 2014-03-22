using System.Runtime.Serialization;

namespace LosPollosHermanos.ServiceContracts
{
    [DataContract]
    public class ProductAvailabilityRequest
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public bool IsAvailable { get; set; }
    }
}