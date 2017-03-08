using System;
using GTANetworkServer;
using GTANetworkShared;

namespace DayZ
{
    public class SpawnManager : Script
    {

        //private readonly Vector3 _copSpawnpoint = new Vector3(447.1f, -984.21f, 30.69f);
        //private readonly Vector3 _crookSpawnpoint = new Vector3(-25.27f, -1554.27f, 300.69f);
        public static readonly Vector3[] spawnpoints = new[] { new Vector3(447.1f, -984.21f, 300.69f), new Vector3(-25.27f, -1554.27f, 300.69f) };

        public SpawnManager()
        {
            API.onClientEventTrigger += ClientEvent;
            API.onPlayerRespawn += PlayerRespawn;
        }

        public void PlayerRespawn(Client player) {
            SpawnPlayer(player);
        }

        public void ClientEvent(Client sender, string eventName, object[] args)
        {

        }


        public void SpawnPlayer(Client target)
        {
            Random random = new Random();
            
            API.setEntityPosition(target, spawnpoints[random.Next(spawnpoints.GetLength(0))]);
			API.givePlayerWeapon(target, WeaponHash.Parachute, 1, true, true);
            API.sendChatMessageToPlayer(target, "~r~Press F to realease your Parachute");
        }
    }
}