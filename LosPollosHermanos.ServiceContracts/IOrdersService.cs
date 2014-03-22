using System.Collections.Generic;
using System.ServiceModel;

namespace LosPollosHermanos.ServiceContracts
{
    [ServiceContract]
    [ServiceKnownType(typeof(OrderRequestLine))]
    public interface IOrdersService
    {
        [OperationContract(IsOneWay = true)]
        void SendOrder(OrderRequest request);

        [OperationContract(IsOneWay = true)]
        void UpdateOrder(UpdateOrderRequest request);

        [OperationContract(IsOneWay = false)]
        IEnumerable<OrderRequest> GetPendingOrders();
    }
}
