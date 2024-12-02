using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(ManageEnemy manageEnemy, ManageWaveSpawn manageWaveSpawn, int waveIndex)
    {
        List<Enemy> enemiesStatus = manageEnemy.ListEnemy();
        List<WaveSpawn> enemySpawnAllWave = manageWaveSpawn.ListWaveSpawn();
        
        WaveSpawn[] enemySpawnOneWave = enemySpawnAllWave.Where(x => x.GetWaveID() == waveIndex).ToArray();


        for (int i = 0; i < enemySpawnOneWave.Length; i++) 
        {
            Enemy addStatus = enemiesStatus.Find(enemyID => enemyID.GetEnemyID() == enemySpawnOneWave[i].GetEnemyID());
        }
    }

    public string TakeNameEnemy(GameObject enemyObject)
    {
        string enemyNameClone = enemyObject.name.ToLower();
        int posWordClone = enemyNameClone.IndexOf("(clone)");
        string enemyName = enemyNameClone.Remove(posWordClone);

        return enemyName;
    }
}