using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WaveSpawn
{
    private int waveSpawnID;
    private int waveID;
    private int enemyID;
    private int spawnCount;
    private double spawnInterval;

    //add data wave spawn from database
    public void AddDataWaveSpawn(IDataReader readEnemyStatus)
    {
        waveSpawnID = Convert.ToInt32(readEnemyStatus["waveSpawnID"]);
        waveID = Convert.ToInt32(readEnemyStatus["waveID"]);
        enemyID = Convert.ToInt32(readEnemyStatus["enemyID"]);
        spawnCount = Convert.ToInt32(readEnemyStatus["spawnCount"]);
        spawnInterval = Convert.ToDouble(readEnemyStatus["spawnInterval"]);
    }

    public int GetWaveID() => waveID;
    public int GetEnemyID() => enemyID;
    public int GetSpawnCount() => spawnCount;
    public double GetSpawnInterval() => spawnInterval;

    public override string ToString()
    {
        return "waveSpawnID: " + waveSpawnID +
                ", waveID: " + waveID +
                ", enemyID: " + enemyID +
                ", spawnCount: " + spawnCount +
                ", spawnInterval: " + spawnInterval;
    }

}
