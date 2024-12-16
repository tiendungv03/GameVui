using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; // Vũ khí mặc định ban đầu (0 là vũ khí đầu tiên)
    public int maxWeapons = 3;     // Giới hạn số lượng vũ khí tối đa

    void Start()
    {
        SelectWeapon(); // Kích hoạt vũ khí đầu tiên
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Đổi súng bằng cuộn chuột
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= maxWeapons - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = maxWeapons - 1;
            else
                selectedWeapon--;
        }

        // Đổi súng bằng phím số
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Phím số 1
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Phím số 2
        {
            selectedWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Phím số 3
        {
            selectedWeapon = 2;
        }

        // Nếu vũ khí được chọn thay đổi, cập nhật lại
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true); // Bật vũ khí được chọn
            else
                weapon.gameObject.SetActive(false); // Tắt các vũ khí còn lại
            i++;
        }
    }
}
