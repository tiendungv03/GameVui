using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBoxInteraction : InteractObject
{
    public Weapon weapon; // Tham chiếu tới Text Object để hiển thị thông báo

    void Update()
    {

        if (isPlayerInRange)
        {
            Debug.Log("Player is in range.");
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed and player is in range. isPlayerInRange: " + isPlayerInRange);
            weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>();
            if (weapon != null && weapon.currentAmmo < weapon.maxCurrentAmmo)
            {
                int restoreAmmo = weapon.maxCurrentAmmo / 2;
                if (weapon.currentAmmo > restoreAmmo) { 
                    weapon.currentAmmo = weapon.maxCurrentAmmo;
                }
                else
                {
                    weapon.currentAmmo += restoreAmmo; // Xóa thông báo sau khi sử dụng hộp tiếp đạn
                }
                Debug.Log("Ammo supplied!");
                Destroy(gameObject); // Xóa hộp tiếp đạn sau khi sử dụng
            }
            else
            {
                Debug.LogError("weapon is null!");
            }
        }
    }
}

