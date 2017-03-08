using System;
using System.Timers;
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
            var mochila = API.createObject(1585260068, new Vector3(-25.27f, -1554.27f, 30.69f), new Vector3(0, 0, 0));
            API.attachEntityToEntity(mochila, player, "10706", new Vector3(0,-0.20,-0.20), new Vector3(0,0,-180));
           
        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
            if (eventName == "logged")
            {
                ResetStats(player);
                UpdateClientValues(player);

            }
            else if (eventName == "headshot") {
                var headshot = API.getEntityData(player, "headhshots");
                headshot += headshot;
                API.setEntityData(player, "headshots", headshot);
                UpdateClientValues(player);
            }
        }

        public void UpdateClientValues(Client player)
        {
            var humanity = API.getEntitySyncedData(player, "humanity");
            var murders = API.getEntitySyncedData(player, "murders");
            var banditsKilled = API.getEntitySyncedData(player, "banditsKilled");
            var headshots = API.getEntitySyncedData(player, "headshots");
            var zombiesKilled = API.getEntitySyncedData(player, "zombiesKilled");
            var blood = API.getEntitySyncedData(player, "blood");
            var temperature = API.getEntitySyncedData(player, "temperature");
            var items = API.getEntitySyncedData(player, "items");
            var name = player.name;
            
            API.triggerClientEvent(player, "stats", true, name, humanity, murders, banditsKilled, headshots, zombiesKilled, blood, temperature, items);
            
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
            API.setEntitySyncedData(player, "humanity", 2500);
            API.setEntitySyncedData(player, "murders", 0);
            API.setEntitySyncedData(player, "banditsKilled", 0);
            API.setEntitySyncedData(player, "headshots", 0);
            API.setEntitySyncedData(player, "zombiesKilled", 0);
            API.setEntitySyncedData(player, "blood", 12000);
            API.setEntitySyncedData(player, "temperature", 25);
            API.setEntitySyncedData(player, "aliveTime", 0);
        }
    }
}