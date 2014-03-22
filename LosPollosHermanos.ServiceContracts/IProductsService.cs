using System.ServiceModel;

namespace LosPollosHermanos.ServiceContracts
{
    [ServiceContract]
    public interface IProductsService
    {
        [OperationContract(IsOneWay = true)]
        void UpdateProductStock(ProductAvailabilityRequest request);

        [OperationContract(IsOneWay = false)]
        AvailableProduct[] GetAvailableProducts();

        [OperationContract(IsOneWay = false)]
        Product[] GetAllProducts();
    }
}
