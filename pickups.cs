using System;
using GTANetworkServer;
using GTANetworkShared;

namespace DayZ
{
    public class PickupsManager : Script
    {

        private readonly Vector3 _copSpawnpoint = new Vector3(447.1f, -984.21f, 30.69f);
        private readonly Vector3 _crookSpawnpoint = new Vector3(-25.27f, -1554.27f, 30.69f);
        private readonly Vector3 _stripClub = new Vector3(126.135, 1278.583, 29.270 );
        private readonly Vector3 _defaultRot = new Vector3(0, 0, 0);

        public PickupsManager()
        {
            API.onClientEventTrigger += ClientEvent;
			API.onResourceStart += ResourceStart;
        }

        public void ClientEvent(Client sender, string eventName, object[] args)
        {
			 Vector3[] positionArray = new [] { new Vector3(0f,0f,0f), new Vector3(1f,1f,1f) };
        }


        public void SpawnPlayer(Client target)
        {
            API.setEntityPosition(target, _crookSpawnpoint);
        }
		
		public void ResourceStart(){
  
                API.createVehicle((VehicleHash)(11251904), new Vector3(-25.27f, -1554.27f, 30.69f), new Vector3(0.431107551, -4.3793354, -104.347885), 132, 132);
				API.createObject(1888204845, new Vector3(-26.27f, -1554.27f, 30.69f), new Vector3(1.00178095E-05, 5.00895658E-06, 93.57354));
                API.createObject(2142033519, new Vector3(-27.27f, -1554.27f, 30.69f), new Vector3(1.00177785E-05, 5.00895567E-06, 149.224045));
                API.createObject(1681727376, new Vector3(-28.27f, -1554.27f, 30.69f), new Vector3(1.00193711E-05, 4.249998, 179.750412));
                API.createObject(1888204845, new Vector3(-29.27f, -1554.27f, 30.69f), new Vector3(1.00178486E-05, -5.00895567E-06, -150.924469));
                API.createObject(2142033519, new Vector3(-30.27f, -1554.27f, 30.69f), new Vector3(1.00178477E-05, -5.008956E-06, -150.524414));
                API.createObject(2142033519, new Vector3(-31.27f, -1554.27f, 30.69f), new Vector3(9.234591E-06, 1.50000012, -113.74984));
                API.createObject(2142033519, new Vector3(-32.27f, -1554.27f, 30.69f), new Vector3(8.19247E-06, 1.124968, -22.6249676));
                API.createObject(1681727376, new Vector3(-33.27f, -1554.27f, 30.69f), new Vector3(1.01394289E-05, -0.749813139, 28.2749119));
                API.createObject(1681727376, new Vector3(-34.27f, -1554.27f, 30.69f), new Vector3(1.31823417E-05, -1.04978132, 130.37468));
                API.createObject(1681727376, new Vector3(-35.27f, -1554.27f, 30.69f), new Vector3(1.37754905E-05, -1.99999714, 117.899727));
                API.createObject(1888204845, new Vector3(-36.27f, -1554.27f, 30.69f), new Vector3(8.784608E-06, 5.000035, -126.97477));
                API.createObject(2142033519, new Vector3(-37.27f, -1554.27f, 30.69f), new Vector3(1.00178413E-05, 5.00895567E-06, 179.075745));
                API.createObject(2142033519, new Vector3(-38.27f, -1554.27f, 30.69f), new Vector3(1.00178322E-05, 5.00895567E-06, 161.475128));
                
		}
		
		[Command]
        public void ShowPos(Client sender)
        {
			Vector3 pos = API.getEntityPosition(sender);
			API.sendChatMessageToPlayer(sender, pos.ToString());
        }
		
		[Command]
        public void Teleport(Client sender, string x, string y, string z)
        {
			Vector3 location = new Vector3(x + y + z);
			API.setEntityPosition(sender, location);
        }
    }
}