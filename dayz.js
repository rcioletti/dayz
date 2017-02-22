var pool = API.getMenuPool();
var isLogged = false;
var health = "12000";
var inventoryWindow = API.createMenu("Inventory", 0, 0, 4);

var items;
var itemSlots = 0;
var usedSlots = 0;
var currentLootId = -1;
var backpack = "";
var lootPos = null;
var hunger = 100;
var thirst = 100;

var name = "";

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
            inventoryWindow.AddItem(item);
        }
    }
    else
        inventoryWindow.Visible = false;
}
