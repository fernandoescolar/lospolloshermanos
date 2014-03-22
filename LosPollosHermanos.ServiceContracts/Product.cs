using System.Runtime.Serialization;

namespace LosPollosHermanos.ServiceContracts
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsAvailable { get; set; }
    }
}