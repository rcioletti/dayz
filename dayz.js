var pool = API.getMenuPool();
var isLogged = false;
var blood = 0;
var name = "";
var humanity = 0;
var murders = 0;
var banditsKilled = 0;
var headshots = 0;
var zoombiesKilled = 0;
var temperature = 0;
var items;
var inventoryWindow = API.createMenu("Inventory", 0, 0, 4);
var debugMonitor = API.createMenu("Debug Monitor:" 0, 0, 6);

API.callNative("SET_FRONTEND_RADIO_ACTIVE", false);

API.onUpdate.connect(function (sender, args) {
    if (pool != null)
    {
        pool.ProcessMenus();
    }
	
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
		zoombiesKilled = args[6];
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

function()

function showDebugMonitor(){
	if(pool){
		
		debugMonitor.Visible = true;
		debugMonitor = API.createMenuItens("Name: " + name);
		debugMonitor = API.createMenuItens("Humanity: " + health);
		debugMonitor = API.createMenuItens("Murders: " + murders);
		debugMonitor = API.createMenuItens("Bandits Killed: " + banditsKilled);
		debugMonitor = API.createMenuItens("Headshots: " + headshot);
		debugMonitor = API.createMenuItens("Zombies Killed: " + zoombiesKilled);
		debugMonitor = API.createMenuItens("Blood: " + blood);
		debugMonitor = API.createMenuItens("Temperature: " + temperature);
		
	}
}

API.onKeyDown.connect(function (sender, e) {
  if (e.KeyCode === Keys.J) {
    openInventory();
  }else if (e.KeyCode === Keys.F5){
	  showDebugMonitor();
  }
})


