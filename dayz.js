var pool = API.getMenuPool();
var isLogged = false;
var blood = 0;
var name = "";
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
	showDebugMonitor(sender);
	
	API.setWeather(2);
	
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
		name = args[1];
        humanity = args[2];
        murders = args[3];
        banditsKilled = args[4];
        headshots = args[5];
		zombiesKilled = args[6];
		blood = args[7];
		temperature = args[8];
		items = args[9];
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

function showDebugMonitor(sender){
	if(isLogged == true){	
		API.drawText("Debug Monitor: ", res_X - 150, 280, 0.2, 50, 205, 50, 255, 0, 1, false, false, 0);
		
		API.drawText("Name: ", res_X - 150, 310, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		
		API.drawText("Humanity: " + humanity, res_X - 150, 340, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		API.drawText("Murders: " + API.getEntityData(sender, "murders"), res_X - 150, 370, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		API.drawText("Bandits Killed: " + banditsKilled, res_X - 150, 400, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		API.drawText("Headshots: " + headshots, res_X - 150, 430, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		API.drawText("Zombies Killed: " + zombiesKilled, res_X - 150, 460, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		API.drawText("Blood: " + blood, res_X - 150, 490, 0.3, 255, 255, 255, 255, 0, 1, false, false, 0);
		
		//DrawHUD
		API.drawRectangle(res_X - 311, 279, 302, 277, 0, 0, 0, 255);
        API.drawRectangle(res_X - 310, 280, 300, 275, 190, 190, 190, 50);
		
	}
}

API.onKeyDown.connect(function (sender, e) {
  if (e.KeyCode === Keys.J) {
    openInventory();
  }
});



