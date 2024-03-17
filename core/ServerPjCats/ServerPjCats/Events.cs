
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
public class Events : Script
{
    public static int CountPlayers = 0;

    //[ServerEvent(Event.Update)]
    //public void Updatehz()
    //{
    //}
    [ServerEvent(Event.ResourceStart)]
    public void OnRecourseStart()
    {
        NAPI.Util.ConsoleOutput("zdarova zaebal V1.2");
        NAPI.Server.SetAutoRespawnAfterDeath(false);
        NAPI.Server.SetDefaultSpawnLocation(new Vector3(-1845, 4585, 10));
        MySQL.Test(); 
        DateTime currentTime = DateTime.Now;
        int currentHour = currentTime.Hour;
        int currentMinute = currentTime.Minute;
        int currentSecond = currentTime.Second;
        NAPI.World.SetTime(currentHour, currentMinute, currentSecond);
        Waypoints.WaypointsListCreate();
        Bussines.BussinesListCreate();


        //string insertQuery = "INSERT INTO users (name) VALUES (@name)";
        //using MySqlCommand insertCommand = new MySqlCommand(insertQuery);
        //insertCommand.Parameters.AddWithValue("@name", "Daleiter");

        //MySQL.Query(insertCommand);

        //string querry = "SELECT * FROM users";
        //using MySqlCommand command = new MySqlCommand(querry);
        //DataTable dt = MySQL.QueryRead(command);
        //foreach(DataRow dr in dt.Rows)
        //{
        //    var cells = dr.ItemArray;
        //    foreach(var cell in cells)
        //    {
        //        NAPI.Util.ConsoleOutput(cell.ToString());
        //    }
        //}
        //        -1841,8368, 4562,1157, 5,2713904
        //0, 0, -145,20427
    }

    


    [ServerEvent(Event.PlayerSpawn)]
    public void OnPlayerSpawn(GTANetworkAPI.Player player)
    {
        
    }

    [ServerEvent(Event.PlayerConnected)]
    public void OnPlayerConnected(GTANetworkAPI.Player player)
    {
        CountPlayers++;
        player.Position = new Vector3(-1847.3251, 4571.128, 5.5569506);
        player.Rotation = new Vector3(0, 0, 0);
        player.Dimension = player.Id;
        (string name, string passhash) = RemoteEvents.getPlayerIP(player.Address);
        if (!string.IsNullOrEmpty(passhash))
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PassHashToAuth", name, passhash);
        }
    }
    [ServerEvent(Event.PlayerDisconnected)]
    public void OnPlayerDisconect(Player player, DisconnectionType type, string reason)
    {
        CountPlayers--;
        try { 
        Vector3 playerPosition = player.Position;
        string selectQuery = "UPDATE users SET posx = @posx, posy = @posy, posz = @posz WHERE name = @name;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
            selectCommand.Parameters.AddWithValue("@name", player.Name);
            selectCommand.Parameters.AddWithValue("@posx", Convert.ToInt32(playerPosition.X));
            selectCommand.Parameters.AddWithValue("@posy", Convert.ToInt32(playerPosition.Y));
            selectCommand.Parameters.AddWithValue("@posz", Convert.ToInt32(playerPosition.Z));
            MySQL.QueryRead(selectCommand);
        }
        catch { }
    }
}