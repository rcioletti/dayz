var isLogged = false;
var id = 0;
var health = 12000;

API.onUpdate.connect(function (sender, args) {
   if(isLogged = true){

   }

});

API.onServerEventTrigger.connect(function (eventName, args) {
    if(eventName = "spawn"){
        id = args[0];
        isLogged = args[1];
        health = args[2];
    }
});