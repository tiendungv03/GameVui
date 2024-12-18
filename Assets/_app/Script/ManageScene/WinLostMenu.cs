using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLostMenu : MonoBehaviour
{
    public GameObject winLostMenu;
    private PlayerHealth playerHealth;
    private TextMeshProUGUI text;
    private WaveDisplay waveDisplay;
    private TimeText timeText;
    private EnemyRemainText enemyRemainText;

    private int maxWave;
    private bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject textMesh = GameObject.FindGameObjectWithTag("WinLostMenu");
        waveDisplay = FindAnyObjectByType<WaveDisplay>();
        timeText = FindAnyObjectByType<TimeText>();
        enemyRemainText = FindAnyObjectByType<EnemyRemainText>();

        if (player == null)
        {
            Debug.LogError("ko co player");
            return;
        }

        if (textMesh == null)
        {
            Debug.LogError("ko co game object tag WinLostMenu");
            return;
        }

        if (waveDisplay == null)
        {
            Debug.LogError("ko co game object tag waveDisplay");
            return;
        }

        if (enemyRemainText == null) 
        {
            Debug.LogError("ko co game object tag enemyRemainText");
            return;
        }

        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        text = textMesh.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        winLostMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.currentHealth <= 0) 
            LoseGame();
        if (maxWave == waveDisplay.waveIndex && enemyRemainText.NumEnemyRemain() <= 0 && timeText.GetTime() <= 0)
            WinGame();
    }

    public void LoseGame()
    {
        PauseGame();
        text.text = "THUA CUỘC";
        text.color = Color.red;
    }

    public void WinGame()
    {
        PauseGame();
        text.text = "THẮNG CUỘC";
        text.color = Color.green;
    }

    public void PauseGame()
    {
        isPaused = true;
        winLostMenu.SetActive(true);
        Cursor.visible = true; // Show cursor
        Cursor.lockState = CursorLockMode.None; // Free cursor
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        winLostMenu.SetActive(false);
        Cursor.visible = false; // Hide cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMaxWave(int waveAmount)
    {
        maxWave = waveAmount;
    }
}
