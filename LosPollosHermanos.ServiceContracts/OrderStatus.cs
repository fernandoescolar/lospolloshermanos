using System.Runtime.Serialization;

namespace LosPollosHermanos.ServiceContracts
{
    [DataContract]
    public enum OrderStatus
    {
        [EnumMember]
        Pending,
        [EnumMember]
        Sent,
        [EnumMember]
        Cooking,
        [EnumMember]
        Delivered
    }
}