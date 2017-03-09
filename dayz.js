var pool = API.getMenuPool();
var isLogged = false;
var blood = 0;
var playerName = "";
var humanity = 0;
var murders = 0;
var banditsKilled = 0;
var headshots = 0;
var zombiesKilled = 0;
var temperature = 0;
var items;
var backpack;
var usedSlots;
var itemSlots;
var itemCounts;
var ping = 0;
var aliveTime = 0;

var damageTable = [ ["M4", 3500],
["CZ 550", 8000],
["Winchester 1866", 3500],
["MP5A5",889],
["SPAZ-12 Combat Shotgun",2000],
["AK-47",2722],
["Lee Enfield",8000],
["Hunting Knife",1500],
["Hatchet",1006],
["M1911",889],
["M9 SD",889],
["PDW",889],
["Sawn-Off Shotgun",2000],
["Desert Eagle",1389],
["Grenade",17998],
["Baseball Bat",953],
["Shovel",953],
["Golf Club",953] ];

var res_X = API.getScreenResolutionMantainRatio().Width;
var res_y = API.getScreenResolutionMantainRatio().Height;

var inventoryWindow = API.createMenu("Inventory", 0, 0, 4);
var debugMonitor = API.createMenu("Debug Monitor:", 0, 0, 6);

API.callNative("SET_FRONTEND_RADIO_ACTIVE", false);

API.onUpdate.connect(function (sender, args) {
    if (pool != null)
    {
        pool.ProcessMenus();
    }
	showDebugMonitor();

    
	checkPlayerPing();
	
});

API.onServerEventTrigger.connect(function (name, args) {
if (name == "createCamera") {
        var pos = args[0];
        var target = args[1];

        var newCam = API.createCamera(pos, new Vector3());
        API.pointCameraAtPosition(newCam, target);
        API.setActiveCamera(newCam);
    }else if(name == "stats"){
		isLogged = args[0];
		playerName = args[1];
        humanity = args[2];
        murders = args[3];
        banditsKilled = args[4];
        headshots = args[5];
		zombiesKilled = args[6];
		blood = args[7];
		temperature = args[8];
		items = args[9];
	}else if(name == "updateAliveTime"){
        setPlayerAliveTime();
    }
});

API.onLocalPlayerDamaged.connect(function(enemy, weapon, bone) {
    if (bone == 31086) // head
    {
        API.triggerServerEvent("headshot", enemy);
        API.setPlayerHealth(-1);
		headshots += headshots;
    }
});

function openInventory()
{
    if (!inventoryWindow.Visible)
    {
        pool = API.getMenuPool();
        inventoryWindow = API.createMenu(backpack + " (" + usedSlots + "/" + itemSlots + ")", -215, 0, 4);
        pool.Add(inventoryWindow);
        inventoryWindow.Visible = true;

        for (var i = 0; i < usedSlots; i++)
        {
            var item = addInventoryItem(itemCounts[i] + ": " + items[i], items[i])
            inventorywindow.AddItem(item);
        }
    }
    else
        inventoryWindow.Visible = false;
}

function showDebugMonitor(){
	if(isLogged == true){

		API.drawText("Debug Monitor: ", res_X - 150, 280, 0.2, 50, 205, 50, 255, 0, 1, false, false, 0);
		API.drawText("Name: " + playerName, res_X - 300, 310, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
		API.drawText("Murders: ", res_X - 300, 340, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
		API.drawText("Zombies Killed: ", res_X - 300, 370, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
        API.drawText("Alive Time: ", res_X - 300, 400, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
        API.drawText("Headshots: ", res_X - 300, 430, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
        API.drawText("Blood: ", res_X - 300, 460, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
        API.drawText("Temperature: ", res_X - 300, 490, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
        API.drawText("Humanity: ", res_X - 300, 520, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
		API.drawText("Bandits Killed: ", res_X - 300, 550, 0.25, 255, 255, 255, 255, 0, 0, false, false, 0);
		
		
		//DrawHUD
		API.drawRectangle(res_X - 311, 279, 302, 302, 0, 0, 0, 255);
        API.drawRectangle(res_X - 310, 280, 300, 300, 190, 190, 190, 20);

        refreshDebugMonitor();
		
	}
}

function refreshDebugMonitor(){
    if(isLogged == true){

        var player = API.getLocalPlayer();

        murders = API.getEntitySyncedData(player, "murders");
        zombiesKilled = API.getEntitySyncedData(player, "zombiesKilled");
        aliveTime = API.getEntitySyncedData(player, "aliveTime");
        headshots = API.getEntitySyncedData(player, "headshots");
        blood = API.getEntitySyncedData(player, "blood");
        temperature = API.getEntitySyncedData(player, "temperature");
        humanity = API.getEntitySyncedData(player, "humanity");
        banditsKilled = API.getEntitySyncedData(player, "banditsKilled");



        API.drawText("" + murders, res_X - 15, 340, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + zombiesKilled, res_X - 15, 370, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + aliveTime, res_X - 15, 400, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + headshots, res_X - 15, 430, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + blood, res_X - 15, 460, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + temperature, res_X - 15, 490, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + humanity, res_X - 15, 520, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
        API.drawText("" + banditsKilled, res_X - 15, 550, 0.25, 255, 255, 255, 255, 0, 2, false, false, 0);
    }
}




function checkPlayerPing(){

var player = API.getLocalPlayer();
ping = API.getPlayerPing(player);

    if(ping > 300){
        API.disconnect("Bad Ping");
    }
}

function setPlayerAliveTime(){
    if(isLogged == true){
    var player = API.getLocalPlayer();
    var hour = "";     

    aliveTime = API.getEntitySyncedData(player, "aliveTime");
    aliveTime += 1;

    if(aliveTime < 60)
    API.setEntitySyncedData(player, "aliveTime", aliveTime);
    }else{
        hour = aliveTime/60;

    API.setEntitySyncedData(player, "aliveTime", hour + aliveTime);
    }
}

API.onKeyDown.connect(function (sender, e) {
  if (e.KeyCode === Keys.J) {
    openInventory();
  }
});




