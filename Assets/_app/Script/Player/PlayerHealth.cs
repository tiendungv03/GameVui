using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Máu tối đa    
    public int currentHealth;  // Máu hiện tại
    public HealthBar healthBar; // Thanh máu
    public Text healthText; // Hiển thị số máu
    /*public float healDuration = 2f; // Thời gian hồi máu từ từ (ví dụ 2 giây)*/

    /*private bool isNearHealingBox = false; // Kiểm tra xem người chơi có gần hộp máu không*/

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu đầy đủ
        healthBar.SetMaxHealth(maxHealth); // Thiết lập thanh máu tối đa
        UpdateHealthUI(); // Cập nhật cả thanh máu và số máu
    }

    // Hồi máu từ từ theo thời gian
    public IEnumerator HealOverTime(int healAmount, float duration)
    {
        float targetHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        float startHealth = currentHealth;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Tính toán phần trăm hồi máu mỗi lần
            currentHealth = Mathf.RoundToInt(Mathf.Lerp(startHealth, targetHealth, elapsedTime / duration));
            healthBar.SetHealth(currentHealth); // Cập nhật thanh máu
            UpdateHealthUI(); // Cập nhật UI số máu

            elapsedTime += Time.deltaTime; // Tăng thời gian đã trôi qua
            yield return null; // Đợi frame tiếp theo
        }

        currentHealth = Mathf.RoundToInt(targetHealth); // Đảm bảo máu không vượt quá maxHealth
        healthBar.SetHealth(currentHealth); // Cập nhật thanh máu
        UpdateHealthUI(); // Cập nhật UI số máu
    }

    public void StartHealing(int healAmount, float healDuration)
    {
        StartCoroutine(HealOverTime(healAmount, healDuration)); // Hồi 20 máu trong 2 giây
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giới hạn giá trị trong khoảng 0 và maxHealth
        healthBar.SetHealth(currentHealth); // Cập nhật thanh máu
        UpdateHealthUI(); // Cập nhật hiển thị số máu

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + Mathf.Clamp(currentHealth, 0, maxHealth).ToString(); // Đảm bảo không âm
        }
    }

    void Die()
    {
        Debug.Log("Player đã chết!");
        Time.timeScale = 0f; // Tạm dừng trò chơi
    }
}