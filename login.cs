using GTANetworkServer;

namespace DayZ
{
    public class LoginManager : Script
    {
        bool registerSucess = false;

        public LoginManager()
        {
            Database.Init();

            API.onResourceStop += onResourceStop;
            API.onPlayerFinishedDownload += onPlayerConnect;
            API.onClientEventTrigger += OnClientEvent;
        }

        public void Login(Client sender, string password)
        {

            if (Database.IsPlayerLoggedIn(sender))
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: ~w~You're already logged in!");
                return;
            }

            if (!Database.TryLoginPlayer(sender, password))
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR:~w~ Wrong password, or account doesnt exist!");
            }
            else
            {
                Database.LoadPlayerAccount(sender);
                API.sendChatMessageToPlayer(sender, "~g~Logged in successfully!");
            }
        }

        public void Register(Client sender, string password)
        {
            if (Database.IsPlayerLoggedIn(sender))
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: ~w~You're already logged in!");
                return;
            }

            if (Database.DoesAccountExist(sender.socialClubName))
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: ~w~An account linked to this Social Club handle already exists!");
                return;
            }

            Database.CreatePlayerAccount(sender, password);
            API.sendChatMessageToPlayer(sender, "~g~Account created! ~w~Now log in with ~y~/login [password]");
            registerSucess = true;
        }

        public void onResourceStop()
        {
            foreach (var client in API.getAllPlayers())
            {
                foreach (var data in API.getAllEntityData(client))
                {
                    API.resetEntityData(client, data);
                }
            }
        }

        public void onPlayerConnect(Client player)
        {
            if (!Database.DoesAccountExist(API.getPlayerName(player))){
                API.triggerClientEvent(player, "notHaveAccount", API.getPlayerName(player));
            }
            else
            {
                API.triggerClientEvent(player, "haveAccount", API.getPlayerName(player));
            }
        }

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
            if (eventName == "register")
            {
                Register(player, arguments[0].ToString());
                if (registerSucess)
                {
                    API.triggerClientEvent(player, "haveAccount", API.getPlayerName(player));
                }

            }
            else if (eventName == "login") {
                Login(player, arguments[0].ToString());
            }
        }
    }
}