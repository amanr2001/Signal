using Microsoft.AspNetCore.SignalR;

namespace Signal.HUB
{
    public class chathub:Hub
    {

        private static Dictionary<int, string> userconnectionstore = new Dictionary<int, string>();
        public override Task OnConnectedAsync()
        {
            string connectionid = Context.ConnectionId;
            int userid = Convert.ToInt32(Context.GetHttpContext().Request.Query["userid"]);
            userconnectionstore[userid] = connectionid;
            return base.OnConnectedAsync();
        }
        public async Task sendmess(string message, int userid)
        {
            await Clients.Client(userconnectionstore[userid]).SendAsync("messagereceived", message);

        }

    }
}
