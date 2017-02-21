var mainMenu = API.createMenu("Login", 0, 0, 4);
var menuPool = API.getMenuPool();


menuPool.Add(mainMenu);

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "notHaveAccount") {

        player = args[0];
        menuPool = API.getMenuPool();

        mainMenu = API.createMenu("Register", "Welcome to the Server " + player + "!", -215, 100, 4);
        var registerButton = API.createMenuItem("Register", "Click to Register!");
        mainMenu.AddItem(registerButton);

        registerButton.Activated.connect(function (menu, item) {
            var passwordString = API.getUserInput("", 25);
            API.triggerServerEvent("register", passwordString)
        });

        menuPool.Add(mainMenu);
        mainMenu.Visible = true;
    }else if(eventName == "haveAccount"){
        player = args[0];
        menuPool = API.getMenuPool();

        mainMenu = API.createMenu("Login", "Welcome to the server " + player + "!", -215, 100, 4);
        var loginButton = API.createMenuItem("Login", "Type your password to Login!");
        mainMenu.AddItem(loginButton);

        loginButton.Activated.connect(function (menu, item) {
            var passwordString = API.getUserInput("", 25);
            API.triggerServerEvent("login", passwordString)
        });

        menuPool.Add(mainMenu);
        mainMenu.Visible = true;
    }
});

API.onUpdate.connect(function(sender, events) {
    menuPool.ProcessMenus();
});