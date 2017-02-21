var isLogged = false;
var health = "12000";

API.onUpdate.connect(function (sender, args) {
    if(isLogged == true){
        API.triggerServerEvent("spawnPoint", sender);
        API.setActiveCamera(null);
    }

});

API.onServerEventTrigger.connect(function (eventName, args) {
    if(eventName == "spawn"){
        isLogged = true;
    }
});

API.onLocalPlayerDamaged.connect(function(enemy, weapon, bone) {
    if (bone == 31086) // head
    {
        API.triggerServerEvent("headshot", enemy);
        API.setPlayerHealth(-1);
    }
});
