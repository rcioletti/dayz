using System;
using GTANetworkServer;


namespace DayZ
{
    public class DayzGamemode : Script
    {
        string id = "1";
        string Health = "12000";
        string isLogged = "1"; 

        public DayzGamemode()
        {

            API.onClientEventTrigger += OnClientEvent;
            API.onPlayerFinishedDownload += onPlayerConnect;
        }

        public void onPlayerConnect(Client player)
        {
          

        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
        }
    }
}