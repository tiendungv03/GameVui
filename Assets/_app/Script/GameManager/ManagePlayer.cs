using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ManagePlayer
{
    private List<Player> players;
    private ManageData manageData;
    private string dbPath;

    public ManagePlayer(string dbPath)
    {
        this.dbPath = dbPath;
        manageData = new ManageData(dbPath);
        players = new List<Player>();
    }

    public void GetPlayerStatus()
    {
        manageData.OpenConnect();

        players = manageData.GetDataFromTable("Player", reader =>
        {
            Player player = new Player();
            player.AddDataStatus(reader);
            return player;
        });
        manageData.CloseConnect();
    }

    public void ShowPlayerStatus()
    {
        foreach (Player player in players)
            Debug.Log(player);
    }

    public Player FirstPlayer() { return players[0]; }
}
