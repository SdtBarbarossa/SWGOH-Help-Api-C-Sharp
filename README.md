# SWGOH-Help-Api-C-Sharp

this is a helper to connect to api.swgoh.help

Usage example:

```
using swgoh_help_api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.SWGOH_Help_Api_C_Sharp_master.connectionHelper;

namespace WebApplication1
{
    public partial class _Default : Page
    {

        UserSettings userSettings;
        swgohHelpApiHelper helper;

        protected void Page_Load(object sender, EventArgs e)
        {
            userSettings = new UserSettings();
            userSettings.password = "NOT_MY_PASSWORD";
            userSettings.username = "sdtbarbarossa";
            helper = new swgohHelpApiHelper(userSettings);
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            helper.login();
            var player = helper.fetchPlayer(new int[]{ 498294819 });
            var guild = helper.fetchGuild(new int[] { 498294819 });
            var units = helper.fetchUnits(new int[] { 498294819 });
            var zetas = helper.fetchZetas();
            var squads = helper.fetchSquads();
            var events = helper.fetchEvents();
            var battles = helper.fetchBattles();
            var data = helper.fetchData(DataEndpointConstants.abilityList);
            var test = data;
        }
    }
}
```
