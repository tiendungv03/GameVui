using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Đặt giá trị tối đa cho thanh máu
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Cập nhật giá trị hiện tại của thanh máu
    public void SetHealth(int health)
    {
        slider.value = Mathf.Clamp(health, 0, slider.maxValue); // Đảm bảo giá trị luôn trong khoảng 0 - maxValue
    }

}

