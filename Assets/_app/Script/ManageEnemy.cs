using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class ManageEnemy
{
    private List<Enemy> enemyList;
    private ManageData manageData;
    public List<GameObject> spawnEnemyRandom;
    private string dbPath;

    public ManageEnemy(string dbPath)
    {
        this.dbPath = dbPath;
        manageData = new ManageData(dbPath);
        enemyList = new List<Enemy>();
    }
    public void GetEnemyStatus()
    {
        manageData.OpenConnect();

        enemyList = manageData.GetDataFromTable("EnemyStatus", reader =>
        {
            Enemy enemy = new Enemy();
            enemy.AddDataStatus(reader);
            return enemy;
        });
        manageData.CloseConnect();
    }

    public List<Enemy> ListEnemy()
    {
        return enemyList;
    }

    public void ShowEnemyStatus()
    {
        foreach (Enemy enemy in enemyList)
            Debug.Log(enemy);
    }
}
