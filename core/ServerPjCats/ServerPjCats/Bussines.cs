using MySql.Data.MySqlClient;
using System;
using GTANetworkAPI;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class BussinesData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Owner { get; set; }
    public int Price { get; set; }
    public int Balance { get; set; }
    public int MoneyGive { get; set; }
    public int Storage { get; set; }
    public Vector3 Position { get; set; }

    public BussinesData(int id, string name, int owner, int price, int balance, int moneygive, int storage, Vector3 position)
    {
        Id = id;
        Name = name;
        Owner = owner;
        Price = price;
        Balance = balance;
        MoneyGive = moneygive;
        Storage = storage;
        Position = position;
    }
}
public class Bussines
{
    private static List<BussinesData> bussinesList = new List<BussinesData>();
    public static void BussinesListCreate()
    {
        Checkbusines();
        foreach (BussinesData bussines in bussinesList)
        {
            createBussines(bussines);
        }
    }
    public static void createBussines(BussinesData thisbussines)
    {
        float scale = 1;
        var colShape = NAPI.ColShape.CreateSphereColShape(thisbussines.Position, scale, 0);
        colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, new Vector3(thisbussines.Position.X, thisbussines.Position.Y, thisbussines.Position.Z - 1f), new Vector3(), new Vector3(), scale, new Color(255, 0, 0, 100), false, 0));
        colShape.SetData<BussinesData>("BussinesData", thisbussines);
        colShape.OnEntityEnterColShape += OnEntityEnterBussines;
        //colShape.OnEntityExitColShape += OnEntityExitColShape;

        //NAPI.Blip.CreateBlip(blipsprite, position, 1f, 0, name, 255, 0f, true, 0, 0);
    }
    public static void OnEntityEnterBussines(GTANetworkAPI.ColShape colShape, GTANetworkAPI.Player player)
    {
        BussinesData idbussines = colShape.GetData<BussinesData>("BussinesData");
        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpointBussines", idbussines.Name, idbussines.Owner, idbussines.Price, idbussines.Balance, idbussines.MoneyGive, idbussines.Storage);
    }
    public static void Checkbusines()
    {
        string selectQuery = "SELECT * FROM bussines";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        DataTable tb = MySQL.QueryRead(selectCommand);
        foreach (DataRow row in tb.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            string name = row["name"].ToString();
            int owner = Convert.ToInt32(row["owner"]);
            int price = Convert.ToInt32(row["price"]);
            int balance = Convert.ToInt32(row["balance"]);
            int moneyGive = Convert.ToInt32(row["moneygive"]);
            int storage = Convert.ToInt32(row["storage"]);
            float posX = Convert.ToSingle(row["posx"]);
            float posY = Convert.ToSingle(row["posy"]);
            float posZ = Convert.ToSingle(row["posz"]);
            bussinesList.Add(new BussinesData(id, name, owner, price, balance, moneyGive, storage, new Vector3(posX, posY, posZ)));

        }
    }
}
