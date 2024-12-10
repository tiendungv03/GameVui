using TMPro;
using UnityEngine;

public class PlayerHealing : InteractObject
{
    public int healAmount = 20; //Lượng máu hôi
    public float healDuration = 2f; // Thời gian hồi máu từ từ (ví dụ 2 giây)
    public PlayerHealth playerHealth;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
    }

    public void PickUp()
    {
        if (playerHealth == null)
        {
            /*Debug.LogWarning("ko co thong tin mau cua nguoi choi");*/
            return;
        }
        if (isPlayerInRange && Input.GetKeyDown(interact))
        {
            playerHealth.StartHealing(healAmount, healDuration);
            Destroy(gameObject);
        }
    }
}
