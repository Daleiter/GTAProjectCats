using GTANetworkAPI;

public class Commands : Script
{
    [Command("getpos")]
    public void Cmd_getpos(Player player)
    {
        Vector3 playerPosition = player.Position;
        Vector3 playerRotation = player.Rotation;
        NAPI.Util.ConsoleOutput($"{playerPosition.X}, {playerPosition.Y}, {playerPosition.Z}");
        NAPI.Util.ConsoleOutput($"{playerRotation.X}, {playerRotation.Y}, {playerRotation.Z}");
    }
    [Command("hp")]
    public void setHp(Player player, int count = 100)
    {
        player.Health = count;
    }
    [Command("armor")]
    public void setArmor(Player player, int count = 100)
    {
        player.Armor = count;
    }
    [Command("kill")]
    public void setKill(Player player, int count = 0)
    {
        player.Health = count;
    }
    [Command("veh")]
    public void veh(Player player, VehicleHash vehicleHash, int color1 = 1, int color2 = 1, string platenumber = "Admin")
    {
        NAPI.Util.ConsoleOutput(vehicleHash.ToString());
        Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
        Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(vehicleHash, new Vector3(PlayerPos.X + 1f, PlayerPos.Y + 2f, PlayerPos.Z + 1f), 10f, color1, color2, platenumber);
        NAPI.Vehicle.SetVehicleNeonState(myveh1, true);
        NAPI.Vehicle.SetVehicleNeonColor(myveh1, 255, 0, 0);
        NAPI.Chat.SendChatMessageToPlayer(player, $"Игроку: {player.Name} | Выдано: {vehicleHash}");

    }
    [Command("car")]
    public void car(Player player, string car, int color1 = 1, int color2 = 1, string platenumber = "Admin")
    {
        NAPI.Util.ConsoleOutput(car.ToString());
        Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
        Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(car), new Vector3(PlayerPos.X + 1f, PlayerPos.Y + 2f, PlayerPos.Z + 1f), 10f, color1, color2, platenumber);
        //NAPI.Vehicle.SetVehicleNeonState(myveh1, true);
        //NAPI.Vehicle.SetVehicleNeonColor(myveh1, 255, 0, 0);
        NAPI.Chat.SendChatMessageToPlayer(player, $"Игроку: {player.Name} | Выдано: {car}");
        player.SetIntoVehicle(myveh1, 0);

    }
}