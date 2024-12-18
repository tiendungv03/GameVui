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

    private string dbPath;

    private ManageData manageData;
    private ManageEnemy manageEnemy;
    private ManageWave manageWave;
    private ManageWaveSpawn manageWaveSpawn;
    private SpawnEnemy spawnEnemy;
    private WaveDisplay waveDisplay;
    private TimeText timeText;
    private EnemyRemainText enemyRemainText;
    private WinLostMenu winLostMenu;
    // Start is called before the first frame update
    void Awake()
    {     
        dbPath = "Data Source=Assets/Database/RageFire.db";
        manageData = new ManageData(dbPath);
        manageEnemy = new ManageEnemy(dbPath);
        manageWave = new ManageWave(dbPath);
        manageWaveSpawn = new ManageWaveSpawn(dbPath);

        spawnEnemy = FindAnyObjectByType<SpawnEnemy>();
        waveDisplay = FindAnyObjectByType<WaveDisplay>();
        timeText = FindAnyObjectByType<TimeText>();
        enemyRemainText = FindAnyObjectByType<EnemyRemainText>();
        winLostMenu = FindAnyObjectByType<WinLostMenu>();

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

        winLostMenu.SetMaxWave(manageWave.GetAmountWave());
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
            
        for (int i = 0; i < enemyTypes.Count; i++)
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

            if (enemy.enemyPrefab == null)
            {
                Debug.Log("ko co prefab ke thu");
                continue;
            }

            if (numEnemySpawnThisWave == null)
                continue;

            numSpawn = numEnemySpawnThisWave.GetSpawnCount();
            spawnInterval = (float)numEnemySpawnThisWave.GetSpawnInterval();

            for (; numSpawn > 0; numSpawn--)
            {
                int randSpawn = UnityEngine.Random.Range(0, spawnPosRandom.Count());

                GameObject enemyObject = Instantiate(enemy.enemyPrefab, spawnPosRandom[randSpawn].position, spawnPosRandom[randSpawn].rotation);
                Enemy enemyStatus = enemyObject.GetComponent<Enemy>();
                if (enemyStatus == null)
                {
                    Debug.LogWarning("ko co script enemy trong prefab clone");
                    break;
                }
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
            int waveIndex = wave.GetWaveNumber();
            int time = wave.GetDuration();
            int numEnemyThisWay = manageWaveSpawn.NumEnemyThisWay(waveIndex);

            SetUIText(waveIndex, time, numEnemyThisWay);

            StartCoroutine(SpawnOneWave(waveIndex));
            yield return new WaitForSeconds(time);
        }
    }

    private void SetUIText(int waveIndex, int time, int numEnemyThisWay)
    {
        if (timeText != null)
            timeText.SetTime(time);
        if (waveDisplay != null)
            waveDisplay.SetWaveText(waveIndex);
        if (enemyRemainText != null)
            enemyRemainText.AddNumEnemyRemain(numEnemyThisWay);
    }
}
