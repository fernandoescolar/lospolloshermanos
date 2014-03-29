namespace LosPollosHermanos.Web
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.ServiceModel;
    using Infrastructure;

    public class MvcApplication : System.Web.HttpApplication
    {
        ServiceHost _productsService;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _productsService = new ServiceHost(typeof(ProductsService));
            if (_productsService.State == CommunicationState.Closed)
                _productsService.Open();

        }
    }
}
