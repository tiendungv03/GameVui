using TMPro;
using UnityEngine;

public class EnemyRemainText : MonoBehaviour
{
    public TextMeshProUGUI enemyRemainText;
    private int numEnemyRemain;

    void Awake()
    {
        numEnemyRemain = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemyRemain > 0)
        {
            enemyRemainText.text = "Enemy: " + numEnemyRemain;
        }
    }

    public void AddNumEnemyRemain(int numEnemy)
    {
        numEnemyRemain += numEnemy;
    }
}
