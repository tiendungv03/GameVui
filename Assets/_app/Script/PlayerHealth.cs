using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Máu tối đa
    public int currentHealth;  // Máu hiện tại
    public HealthBar healthBar; // Thanh máu
    public Text healthText; // Hiển thị số máu

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu đầy đủ
        healthBar.SetMaxHealth(maxHealth); // Thiết lập thanh máu tối đa
        UpdateHealthUI(); // Cập nhật cả thanh máu và số máu
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
