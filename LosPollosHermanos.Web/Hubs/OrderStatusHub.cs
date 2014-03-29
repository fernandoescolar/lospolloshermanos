using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace LosPollosHermanos.Web.Hubs
{
    [HubName("orderStatus")]
    public class OrderStatusHub : Hub
    {
        public void ListenForUpdates(string clientId)
        {
            Groups.Add(base.Context.ConnectionId, clientId);
        }
    }
}