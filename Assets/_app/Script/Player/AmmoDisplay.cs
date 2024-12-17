using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public int maxAmmoInManagazine = 30; // Số lượng đạn tối đa trong băng đạn
    public int ammoInManagazine; // Số lượng đạn hiện tại
    public int currentAmmo = 50; // số lượng đạn hiện tại trong túi
    public int maxCurrentAmmo = 300;
    public TextMeshProUGUI ammoText; // Text Object để hiển thị số lượng đạn
    private float fireRate = 0.2f; // Tốc độ bắn (giây giữa các lần bắn)
    private float nextFireTime = 0f; // Thời gian tiếp theo có thể bắn
    public float reloadTime = 2f; // Thời gian nạp đạn (giây)
    private bool isReloading = false; // Biến kiểm tra trạng thái nạp đạn

    void Start()
    {
        
        ammoInManagazine = maxAmmoInManagazine; // Khởi tạo số lượng đạn hiện tại bằng số lượng đạn tối đa
        UpdateAmmoDisplay(); // Cập nhật hiển thị số lượng đạn
    }

    void Update()
    {
        if (isReloading)
            return; // Nếu đang nạp đạn thì không thực hiện bắn

        // Kiểm tra nếu người chơi giữ nút bắn (mặc định là nút chuột trái)
        if (Input.GetButton("Fire1") && nextFireTime <= 0 && ammoInManagazine > 0)
        {
            nextFireTime = fireRate; // Cập nhật thời gian tiếp theo có thể bắn
            ammoInManagazine--;
            UpdateAmmoDisplay();
        }
        if (nextFireTime > 0)
        {
            nextFireTime -= Time.deltaTime;
        }

        // Kiểm tra nếu người chơi nạp đạn
        if (Input.GetKeyDown(KeyCode.R) || ammoInManagazine == 0)
        {
            StartCoroutine(Reload()); // Bắt đầu Coroutine nạp đạn
        }
    }

    IEnumerator Reload()
    {
        isReloading = true; // Đặt trạng thái đang nạp đạn
        
        yield return new WaitForSeconds(reloadTime); // Chờ thời gian nạp đạn
        Debug.Log("Reloading...");

        if (ammoInManagazine + currentAmmo >= 40)
        {
            currentAmmo = currentAmmo + ammoInManagazine - maxAmmoInManagazine;
            ammoInManagazine = maxAmmoInManagazine; // Nạp lại đạn
        }
        else
        {
            ammoInManagazine += currentAmmo;
            currentAmmo = 0;
        }
        
        isReloading = false; // Hoàn tất nạp đạn
        UpdateAmmoDisplay(); // Cập nhật hiển thị số lượng đạn
        Debug.Log("Reload complete!");

    }

    void UpdateAmmoDisplay()
    {
        ammoText.text = "Ammo: " + ammoInManagazine + "/" + currentAmmo; // Hiển thị số lượng đạn
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxCurrentAmmo); // Thêm đạn và đảm bảo không vượt quá đạn tối đa
        UpdateAmmoDisplay(); // Cập nhật hiển thị số lượng đạn
    }
}
