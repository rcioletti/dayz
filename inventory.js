var items;
var backpack;
var items;
var itemSlots = 0;
var usedSlots = 0;
var inventory = API.createMenu("Inventory", -215, 0, 6);
var pool = API.getMenuPool();
var list = new List(String);
list.Add("item1");
list.Add("item2");
list.Add("item3");
API.createListItem("Item list", "Description", list, 0);




function openInventory()
{
    if (!inventory.Visible)
    {
        pool = API.getMenuPool();
        inventory = API.createMenu(backpack + " (" + usedSlots + "/" + itemSlots + ")", -215, 0, 4);
        pool.Add(inventory);
        inventory.Visible = true;

        for (var i = 0; i < usedSlots; i++)
        {
            var item = addInventoryItem(itemCounts[i] + ": " + items[i], items[i])
            inventory.AddItem(item);
        }
    }
    else
        inventory.Visible = false;
}


API.onKeyDown.connect(function (sender, e) {
  if (e.KeyCode === Keys.J) {
    openInventory();
  }
});


