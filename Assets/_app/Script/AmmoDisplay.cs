using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    public int ammoInManagazine; // số lượng đạn trong băng đạn
    public int maxAmmoInManagazine = 40; // số lượng đạn tối đa trong băng đạn 
    public int currentAmmo = 200; // số lượng đạn hiện tại trong túi
    public TextMeshProUGUI ammoText; // text hiển thị số lượng đạn
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoInManagazine = maxAmmoInManagazine;
        UpdateAmmoDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ammoInManagazine > 0)
        {
            ammoInManagazine--;
            UpdateAmmoDisplay();
        }
        if (Input.GetKeyDown(KeyCode.R) || ammoInManagazine == 0)
        {
            currentAmmo = currentAmmo + ammoInManagazine - maxAmmoInManagazine;
            ammoInManagazine = maxAmmoInManagazine;
            UpdateAmmoDisplay();
        }
    }
    public void UpdateAmmoDisplay()
    {
        ammoText.text = ammoInManagazine + " / " + currentAmmo;
    }
}
