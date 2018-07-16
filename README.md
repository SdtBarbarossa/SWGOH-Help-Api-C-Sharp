# SWGOH-Help-Api-C-Sharp

this is a helper to connect to api.swgoh.help

Usage example:

UserSettings myUser = new UserSettings();
myUser.username = "YOURUSERNAME";
myUser.password = "YOURPASSWORD";

swgohHelpApiHelper myhelper = new swgohHelpApiHelper(myUser);
myhelper.login();

Player player = myhelper.fetchPlayer(498294819);
List<Player> guild = myhelper.fetchGuild(498294819);
string data = myhelper.fetchData("units");
