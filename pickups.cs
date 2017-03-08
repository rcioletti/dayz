using System;
using GTANetworkServer;
using GTANetworkShared;
using System.Timers;
using System.Collections.Generic;

namespace DayZ
{

    public class PickupsManager : Script
    {

        public static readonly string[,] residential = new string[,]{{"Bandage", "1256177865", "1", "0", "4"},
                                                                      {"WeaponKnife", "170053282", "1", "90", "2"},
                                                                      {"M1911", "-929681224", "1", "90", "1"},
                                                                      {"Empty Gas Cannister", "-1730917948", "1", "90", "5"} };
        public static readonly string[,] industrial = new string[,]{{"Bandage", "1256177865", "1", "0", "4"},
                                                                      {"WeaponKnife", "170053282", "1", "90", "2"},
                                                                      {"M1911", "-929681224", "1", "90", "1"},
                                                                      {"Empty Gas Cannister", "-1730917948", "1", "90", "5"} };
        public static readonly string[,] farm = new string[,]{{"Bandage", "1256177865", "1", "0", "4"},
                                                                      {"WeaponKnife", "170053282", "1", "90", "2"},
                                                                      {"M1911", "-105925489", "1", "90", "1"},
                                                                      {"Empty Gas Cannister", "-962731009", "1", "90", "5"} };
        public static readonly string[,] supermarket = new string[,]{{"Bandage", "1256177865", "1", "0", "4"},
                                                                      {"WeaponKnife", "663586612", "1", "90", "2"},
                                                                      {"M1911", "-105925489", "1", "90", "1"},
                                                                      {"Empty Gas Cannister", "-962731009", "1", "90", "5"} };
        public static readonly string[,] military = new string[,]{{"Bandage", "1256177865", "1", "0", "4"},
                                                                      {"WeaponKnife", "663586612", "1", "90", "2"},
                                                                      {"M1911", "-105925489", "1", "90", "1"},
                                                                      {"Empty Gas Cannister", "-962731009", "1", "90", "5"} };
        public static readonly string[][,] itemTable = new string[][,] {residential, industrial, farm, supermarket, military};

        
      //  {{"Bandage", 341217064, 1, 0, 4},
      //                                                                {"WeaponKnife", 663586612,1, 90, 2},
      //                                                                {"M1911", -105925489, 1, 90, 1.35},
      //                                                                {"Empty Gas Cannister", -962731009, 1, 90, 5} };


        public PickupsManager()
        {
            API.onClientEventTrigger += ClientEvent;
            API.onResourceStart += ResourceStart;
        }

        public void ClientEvent(Client sender, string eventName, object[] args)
        {
        }

        public void ResourceStart()
        {
			
            createPickupsOnServerStart();
			
        }
        public void createItemLoot(Vector3 lootPlace, int id, int lootType)
        {
            var handle = API.createSphereColShape(lootPlace, 5.0f);
            var col = API.createTextLabel("ItemLoot", lootPlace, 5.0F, 1.0F);
            
            API.setTextLabelColor(col, 189, 183, 107, 100);
            

            API.setEntityData(col, "itemloot", true);
            API.setEntityData(col, "parent", lootType);
            API.setEntityData(col, "MAX_Slots", 12);

            API.consoleOutput(API.hasEntityData(col ,"itemloot").ToString());

                 for (int i = 0; i < itemTable[lootType].GetLength(0); i++)
                {
                Random random = new Random();
                int percent = Int32.Parse(itemTable[lootType][i, 4]);
				
				API.consoleOutput(percent.ToString());
				
                var value = percentChance(percent, random.Next(1, 2));
                    API.setEntityData(col, itemTable[lootType][i, 0], value);
                }

            refreshItemLoot(col, lootType);
        }

        public void refreshItemLoot(NetHandle col, int lootType)
        {
            var objects = API.getEntityData(col, "objectsINloot");

            if (objects != null)
            {
                if(objects[1] != null){
                    API.deleteEntity(objects[1]);
                }
                if (objects[2] != null)
                {
                    API.deleteEntity(objects[2]);
                }
                if (objects[3] != null)
                {
                    API.deleteEntity(objects[3]);
                }
            }

            var count = 0;
            NetHandle[] objectItem = new NetHandle[4];

            for (int i = 0; i < itemTable[lootType].GetLength(0); i++)
            {
                    if (API.hasEntityData(col, itemTable[lootType][i, 0]))
                    {
                        if (count == 3)
                        {
                            return;
                        }
                        count = count + 1;
                        Vector3 position = API.getEntityPosition(col);
						Vector3 rotation = new Vector3(0, 0, 0);
						if(itemTable[lootType][i,3] == "90"){
							rotation = new Vector3(90,90,90);
						}
                        int obj = Int32.Parse(itemTable[lootType][i, 1]);
                        float scale = Int32.Parse(itemTable[lootType][i, 2]); 
                        objectItem[count] = API.createObject(obj, position, rotation);
                        //API.setBlipScale(objectItem[count], scale);
                        API.setEntityCollisionless(objectItem[count], true);
                        API.setEntityPositionFrozen(objectItem[count], true);
                    }
            }
            if (objectItem[1] == null)
            {
                Vector3 position = API.getEntityPosition(col);
                objectItem[1] = API.createObject(-1920611843, position, new Vector3(0, 0, 0));
                //API.setBlipScale(objectItem[1], 0);
                API.setEntityCollisionless(objectItem[1], true);
                API.setEntityPositionFrozen(objectItem[1], true);
            }
            if (objectItem[2] == null)
            {
                Vector3 position = API.getEntityPosition(col);
                objectItem[2] = API.createObject(-1920611843, position, new Vector3(0, 0, 0));
                //API.setBlipScale(objectItem[2], 0);
                API.setEntityCollisionless(objectItem[2], true);
                API.setEntityPositionFrozen(objectItem[2], true);
            }
            if (objectItem[3] == null)
            {
                Vector3 position = API.getEntityPosition(col);
                objectItem[3] = API.createObject(-1920611843, position, new Vector3(0, 0, 0));
                //API.setBlipScale(objectItem[3], 0);
                API.setEntityCollisionless(objectItem[3], true);
                API.setEntityPositionFrozen(objectItem[3], true);
            }
            

        }

        public int percentChance(int percent, int reapeatTime)
        {
            int hits = 0;
            for (int i = 1; i == reapeatTime; i++)
            {
                Random number = new Random();
                int number1 = number.Next(0, 200);
                if (percent >= number1)
                {
                    hits = hits + 1;
                }
            }

            return hits;
        }

        public void createPickupsOnServerStart()
        {
            var iPickup = 0;
            foreach (Vector3 position in EditorManager.residential)
            {
                iPickup = iPickup + 1;
                createItemLoot(position, iPickup, 0);
            }
            createPickupsOnServerStart2();
           
            API.consoleOutput("Residential Created!");
        }
        public void createPickupsOnServerStart2()
        {
            var iPickup = 0;
            foreach (Vector3 position in EditorManager.industrial)
            {
                iPickup = iPickup + 1;
                createItemLoot(position, iPickup, 1);
            }
            createPickupsOnServerStart3();

            API.consoleOutput("Industrial Created!");
        }
        public void createPickupsOnServerStart3()
        {
            var iPickup = 0;
            foreach (Vector3 position in EditorManager.farm)
            {
                iPickup = iPickup + 1;
                createItemLoot(position, iPickup, 2);
            }
            createPickupsOnServerStart4();

            API.consoleOutput("Farm Created!");
        }
        public void createPickupsOnServerStart4()
        {
            var iPickup = 0;
            foreach (Vector3 position in EditorManager.supermarket)
            {
                iPickup = iPickup + 1;
                createItemLoot(position, iPickup, 3);
            }
            createPickupsOnServerStart5();

            API.consoleOutput("Supermarket Created!");
        }
        public void createPickupsOnServerStart5()
        {
            var iPickup = 0;
            foreach (Vector3 position in EditorManager.military)
            {
                iPickup = iPickup + 1;
                createItemLoot(position, iPickup, 4);
            }
            API.consoleOutput("Military Created!");
        }

        public void refreshItemsLoot()
        {
            API.consoleOutput("#ffaa00WARNING! #ffffff - SPAWNPOINTS FOR ITEMS ARE BEING REFRESHED! BEWARE OF MASSIVE LAG!", 255, 255, 255);

        }

        public void refreshItemsLootPoints()
        {

        }

    //    public void getItemTablePosition(string itema)
    //    {
    //        foreach (string item in itemTable["other"])
    //        {
    //            if (itema == item[1])
    //            {
    //                return id, "other";
    //            }
    //        }
    //    }

        [Command]
        public void ShowPos(Client sender)
        {
            Vector3 pos = API.getEntityPosition(sender);
            API.sendChatMessageToPlayer(sender, pos.ToString());
        }

        [Command]
        public void Teleport(Client sender, string location)
        {

            // split the items
            string[] sArray = location.Split(',');

            // store as a Vector3
            Vector3 vector = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));

            API.setEntityPosition(sender, vector);
        }
    }
}