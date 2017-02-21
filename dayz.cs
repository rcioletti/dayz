using System;
using GTANetworkServer;


namespace DayZ
{
    public class DayzGamemode : Script
    {
        int id = 1;
        int Health = 12000;
        bool isLogged = true; 

        public DayzGamemode()
        {

            API.onClientEventTrigger += OnClientEvent;
            API.onPlayerFinishedDownload += onPlayerConnect;
        }

        public void onPlayerConnect(Client player)
        {
            if (Database.IsPlayerLoggedIn(player))
            {
                API.triggerClientEvent(player, "spawn", id, isLogged, Health);
            }

        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
        }
    }
}