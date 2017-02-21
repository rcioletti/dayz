var menu = API.createMenu("Login", 0, 0, 4);
var pool = API.getMenuPool();


pool.Add(menu);

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName == "notHaveAccount") {

        player = args[0];
        pool = API.getMenuPool();

        menu = API.createMenu("Register", "Welcome to DayZ " + player + "!", -215, 100, 4);
        var registerButton = API.createMenuItem("Register", "Click to Register!");
        menu.AddItem(registerButton);

        registerButton.Activated.connect(function (menu, item) {
            var passwordString = API.getUserInput("", 25);
            API.triggerServerEvent("register", passwordString)
        });

        pool.Add(menu);
        menu.Visible = true;
    }else if(eventName == "haveAccount"){
        player = args[0];
        pool = API.getMenuPool();

        menu = API.createMenu("Login", "Welcome to DayZ " + player + "!", -215, 100, 4);
        var loginButton = API.createMenuItem("Login", "Type your password to Login!");
        menu.AddItem(loginButton);

        loginButton.Activated.connect(function (menu, item) {
            var passwordString = API.getUserInput("", 25);
            API.triggerServerEvent("login", passwordString)
        });

        pool.Add(menu);
        menu.Visible = true;
    }else if(eventName == "logged"){
        menu.visible = false;
    }
});

API.onUpdate.connect(function(sender, events) {
    pool.ProcessMenus();
});