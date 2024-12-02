using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab;
        private Enemy enemyStatus;
        private List<WaveSpawn> waveSpawn;

        public void SetEnemy(Enemy status) 
        { 
            this.enemyStatus = status;
        }
        public Enemy GetEnemy() {
            return enemyStatus; 
        }
        public void SetWaveSpawn(List<WaveSpawn> spawnPerWave) { waveSpawn = spawnPerWave; }
        public List<WaveSpawn> GetWaveSpawn() => waveSpawn;

        public void ShowEnemyData()
        {
            Enemy enemyObject = enemyPrefab.GetComponent<Enemy>();
            Debug.Log($"enemyPrefab {enemyPrefab.ToString()},enemyStatus: {enemyPrefab}");
            foreach (WaveSpawn waveSpawn in waveSpawn) {
                Debug.Log(waveSpawn);
            }
        }
    }

    public List<EnemyData> enemyTypes;
    public List<Transform> spawnPosRandom;

    private int waveIndex;
    private string dbPath;

    private ManageData manageData;
    private ManageEnemy manageEnemy;
    private ManageWave manageWave;
    private ManageWaveSpawn manageWaveSpawn;
    private SpawnEnemy spawnEnemy;
    // Start is called before the first frame update
    void Awake()
    {     
        dbPath = "Data Source=Assets/Database/RageFire.db";
        manageData = new ManageData(dbPath);
        manageEnemy = new ManageEnemy(dbPath);
        manageWave = new ManageWave(dbPath);
        manageWaveSpawn = new ManageWaveSpawn(dbPath);
        spawnEnemy = FindAnyObjectByType<SpawnEnemy>();

        manageData.OpenConnect();
        manageData.CreateDb();
        manageData.InsertDb();
        manageData.CloseConnect();

        manageEnemy.GetEnemyStatus();
        manageEnemy.ShowEnemyStatus();

        manageWave.GetWaves();
        manageWave.ShowWaves();

        manageWaveSpawn.GetWaveSpawns();
        manageWaveSpawn.ShowWavesSpawns();

        TransferData();
    }

    public void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TransferData()
    {
        List<Enemy> enemies = manageEnemy.ListEnemy();
        List<WaveSpawn> waveSpawns = manageWaveSpawn.ListWaveSpawn();
            
        for (int i = 0; i < enemies.Count; i++)
        {
            List<WaveSpawn> spawnPerWave = new List<WaveSpawn>();
            spawnPerWave = waveSpawns.Where(spawn => spawn.GetEnemyID() == enemies[i].GetEnemyID()).ToList();

            enemyTypes[i].SetEnemy(enemies[i]);
            enemyTypes[i].SetWaveSpawn(spawnPerWave);
        }
    }

    private IEnumerator SpawnOneWave(int waveIndex)
    {
        foreach (EnemyData enemy in enemyTypes)
        {
            int numSpawn;
            float spawnInterval;
            WaveSpawn numEnemySpawnThisWave = enemy.GetWaveSpawn().Find(wave => wave.GetWaveID() == waveIndex);

            if (numEnemySpawnThisWave == null)
                continue;

            numSpawn = numEnemySpawnThisWave.GetSpawnCount();
            spawnInterval = (float)numEnemySpawnThisWave.GetSpawnInterval();

            for (; numSpawn > 0; numSpawn--)
            {
                int randSpawn = UnityEngine.Random.Range(0, spawnPosRandom.Count());

                GameObject enemyObject = Instantiate(enemy.enemyPrefab, spawnPosRandom[randSpawn].position, spawnPosRandom[randSpawn].rotation);
                Enemy enemyStatus = enemyObject.GetComponent<Enemy>();
                enemyStatus.SetEnemyStatus(enemy.GetEnemy());

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    private IEnumerator SpawnWaves()
    {
        List<Wave> waves = manageWave.GetListWave();

        foreach (Wave wave in waves)
        {
            waveIndex = wave.GetWaveNumber();
            Debug.Log(waveIndex);
            StartCoroutine(SpawnOneWave(waveIndex));
            yield return new WaitForSeconds(wave.GetDuration());
        }
    }
}
