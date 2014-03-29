using System.Configuration;

namespace LosPollosHermanos.Infrastructure
{
    public static class SubscriptionInitializer
    {
        public static InitializationRequest Initialize()
        {
            return new InitializationRequest
            {
                Issuer = ConfigurationManager.AppSettings["SERVICE_BUS_ISSUER"],
                IssuerKey = ConfigurationManager.AppSettings["SERVICE_BUS_ISSUER_KEY"],
                Namespace = ConfigurationManager.AppSettings["SERVICE_BUS_NAMESPACE"]
            };
        }
    }
}
