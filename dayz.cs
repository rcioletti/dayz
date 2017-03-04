using System;
using GTANetworkServer;
using GTANetworkShared;


namespace DayZ
{
    public class DayzGamemode : Script
    {

        public DayzGamemode()
        {

            API.onClientEventTrigger += OnClientEvent;
            API.onPlayerFinishedDownload += onPlayerConnect;
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            
        }

        public void onPlayerConnect(Client player)
        {
          
			API.setPlayerSkin(player, PedHash.BikeHire01);
            API.createVehicle(VehicleHash.Akuma, new Vector3(-25.27f, -1554.27f, 30.69f), new Vector3(30,30,30), 1,1);
            API.createObject(170053282, new Vector3(-25.27f, -1554.27f, 30.69f), new Vector3(0, 0, 90));
           
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