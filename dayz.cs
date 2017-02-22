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
          
			API.setPlayerSkin(player, PedHash.BikeHire01);

        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
        }

        public void UpdateClientValues(Client player)
        {
            API.triggerClientEvent(player, "stats");
        }
    }
}