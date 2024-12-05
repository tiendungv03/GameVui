using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageWaveSpawn
{
    private List<WaveSpawn> waveList;
    private ManageData manageData;
    private string dbPath;

    public ManageWaveSpawn(string dbPath)
    {
        this.dbPath = dbPath;
        manageData = new ManageData(dbPath);
        waveList = new List<WaveSpawn>();
    }
    public void GetWaveSpawns()
    {
        manageData.OpenConnect();
        waveList = manageData.GetDataFromTable("WaveSpawns", reader =>
        {
            WaveSpawn waveSpawn = new WaveSpawn();
            waveSpawn.AddDataWaveSpawn(reader);
            return waveSpawn;
        });
        manageData.CloseConnect();
    }

    public List<WaveSpawn> ListWaveSpawn() => waveList;

    public int NumEnemyThisWay(int wave)
    {
        int numEnemy = 0;
        numEnemy = waveList.Where(numThisWay => numThisWay.GetWaveID() == wave).Sum(numThisWay => numThisWay.GetSpawnCount());
        return numEnemy;
    }

    public void ShowWavesSpawns()
    {
        foreach (WaveSpawn wave in waveList)
            Debug.Log(wave);
    }
}
