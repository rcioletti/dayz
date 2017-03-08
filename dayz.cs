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
            API.onPlayerDeath += PlayerDeath;
        }

        private void API_onResourceStart()
        {
            
        }

        public void onPlayerConnect(Client player)
        {
          
			API.setPlayerSkin(player, PedHash.BikeHire01);
            API.createVehicle(VehicleHash.Sanchez, new Vector3(-25.27f, -1554.27f, 30.69f), new Vector3(30,30,30), 1,1);
            //API.createObject(170053282, new Vector3(-25.27f, -1554.27f, 30.69f), new Vector3(90, 90, 90));
           
        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
            if(eventName == "logged")
            {
                UpdateClientValues(player);
                ResetStats(player);
            }
        }

        public void UpdateClientValues(Client player)
        {
            var humanity = API.getEntityData(player, "humanity");
            var murders = API.getEntityData(player, "murders");
            var banditsKilled = API.getEntityData(player, "banditsKilled");
            var headshots = API.getEntityData(player, "headshots");
            var zombiesKilled = API.getEntityData(player, "zombiesKilled");
            var blood = API.getEntityData(player, "blood");
            var temperature = API.getEntityData(player, "temperature");
            var items = API.getEntityData(player, "items");

            API.triggerClientEvent(player, "stats", true, player.name, humanity, murders, banditsKilled, headshots, zombiesKilled, blood, temperature, items);
            
        }

        private void PlayerDeath(Client player, NetHandle entityKiller, int weapon) {
            Client killer = API.getPlayerFromHandle(entityKiller);
            ResetStats(player);
            if (killer != null)
            {
                API.sendNotificationToAll(killer.name + " has killed " + player.name);
            }
            else
            {
                API.sendNotificationToAll(player.name + " died");
            }
        }
        private void ResetStats(Client player) {
            API.setEntityData(player, "humanity", 2500);
            API.setEntityData(player, "murders", 0);
            API.setEntityData(player, "banditsKilled", 0);
            API.setEntityData(player, "headshots", 0);
            API.setEntityData(player, "zombiesKilled", 0);
            API.setEntityData(player, "blood", 12000);
            API.setEntityData(player, "temperature", 25);



        }
    }
}