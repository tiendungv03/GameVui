
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dùng UI Text (TextMeshPro nếu dùng TMP)

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;  // Vị trí để gắn vũ khí (tay của player)
    public GameObject[] weapons;   // Danh sách các vũ khí (Prefab hoặc các đối tượng trong Scene)
    public Text weaponNameText;    // Text UI để hiển thị tên vũ khí (hoặc dùng TextMeshPro nếu thích)
    public GameObject currentWeapon;  // Vũ khí hiện tại (GameObject)

    private string[] weaponNames;  // Mảng tên vũ khí sẽ được tạo động từ tên GameObject
    private int currentWeaponIndex = 0; // Vũ khí hiện tại (mặc định là vũ khí đầu tiên)

    // Các vũ khí rơi trên mặt đất
    public GameObject droppedWeaponPrefab;

    // Các đối tượng cần nhặt lại
    private GameObject droppedWeapon;

    void Start()
    {
        // Khởi tạo mảng weaponNames dựa trên tên của các vũ khí
        weaponNames = new string[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            // Gán tên vũ khí bằng tên GameObject
            weaponNames[i] = weapons[i].name;
        }

        // Đảm bảo tất cả vũ khí bị ẩn, chỉ hiện vũ khí đầu tiên khi khởi động
        EquipWeapon(0);  // Trang bị vũ khí đầu tiên ngay khi bắt đầu
    }

    void Update()
    {
        // Kiểm tra phím bấm để thay đổi vũ khí
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Phím 1 để chọn vũ khí đầu tiên
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Phím 2 để chọn vũ khí thứ hai
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Phím 3 để chọn vũ khí thứ ba
        {
            EquipWeapon(2);
        }

        // Bỏ vũ khí khi nhấn G
        if (Input.GetKeyDown(KeyCode.G) && currentWeapon != null)
        {
            DropWeapon();
        }

        // Nhặt vũ khí khi nhấn F và gần vũ khí rơi
        if (Input.GetKeyDown(KeyCode.F) && droppedWeapon != null)
        {
            PickupWeapon();
        }
    }

    // Phương thức để trang bị vũ khí
    void EquipWeapon(int weaponIndex)
    {
        // Nếu chỉ số vũ khí được chọn giống với vũ khí hiện tại, không cần thay đổi
        if (currentWeaponIndex == weaponIndex) return;

        // Ẩn vũ khí hiện tại
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Hiện vũ khí mới
        if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            currentWeapon = weapons[weaponIndex];
            currentWeapon.SetActive(true);
            currentWeaponIndex = weaponIndex;

            // Cập nhật tên vũ khí trên UI
            UpdateWeaponNameUI();
        }
    }

    // Phương thức để cập nhật tên vũ khí trên giao diện UI
    void UpdateWeaponNameUI()
    {
        if (weaponNameText != null && currentWeaponIndex < weaponNames.Length)
        {
            weaponNameText.text = weaponNames[currentWeaponIndex];
        }
    }

    // Phương thức bỏ vũ khí ra
    void DropWeapon()
    {
        if (currentWeapon != null)
        {
            // Tạo một bản sao của vũ khí đã trang bị để rơi ra đất
            droppedWeapon = Instantiate(currentWeapon, transform.position + transform.forward * 2, Quaternion.identity);
            droppedWeapon.SetActive(true);

            // Tắt vũ khí trên tay player
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

    // Phương thức nhặt vũ khí
    void PickupWeapon()
    {
        if (droppedWeapon != null)
        {
            // Nếu đã có vũ khí trong tay, bỏ vũ khí hiện tại xuống trước khi nhặt vũ khí mới
            if (currentWeapon != null)
            {
                DropWeapon();  // Bỏ vũ khí hiện tại xuống đất
            }

            // Nhặt vũ khí mới
            currentWeapon = droppedWeapon;
            currentWeapon.SetActive(true);

            // Di chuyển vũ khí vào tay player
            currentWeapon.transform.position = weaponHolder.position;
            currentWeapon.transform.rotation = weaponHolder.rotation;
            currentWeapon.transform.SetParent(weaponHolder);

            // Cập nhật lại vũ khí đang trang bị
            droppedWeapon = null;
            EquipWeapon(currentWeaponIndex);  // Đảm bảo tên vũ khí được cập nhật

            // Thêm vũ khí vào danh sách vũ khí đang sở hữu
            AddWeaponToInventory(currentWeapon);
        }
    }

    // Thêm vũ khí vào danh sách vũ khí của player (nếu cần)
    void AddWeaponToInventory(GameObject weapon)
    {
        // Thêm logic vào đây để quản lý các vũ khí trong danh sách của player
        // Ví dụ, bạn có thể bổ sung vũ khí vào mảng hoặc danh sách tùy thuộc vào thiết kế của bạn
    }
}

//DUNG

*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dùng UI Text (TextMeshPro nếu dùng TMP)

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;  // Vị trí để gắn vũ khí (tay của player)
    public GameObject[] weapons;   // Danh sách các vũ khí (Prefab hoặc các đối tượng trong Scene)
    public Text weaponNameText;    // Text UI để hiển thị tên vũ khí (hoặc dùng TextMeshPro nếu thích)
    public GameObject currentWeapon;  // Vũ khí hiện tại (GameObject)

    private string[] weaponNames;  // Mảng tên vũ khí sẽ được tạo động từ tên GameObject
    private int currentWeaponIndex = -1; // Vũ khí hiện tại (-1 khi không có vũ khí)

    // Các vũ khí rơi trên mặt đất
    public GameObject droppedWeaponPrefab;

    // Các đối tượng cần nhặt lại
    private GameObject droppedWeapon;

    public static object Instance { get; internal set; }

    void Start()
    {
        // Khởi tạo mảng weaponNames dựa trên tên của các vũ khí
        weaponNames = new string[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            // Gán tên vũ khí bằng tên GameObject
            weaponNames[i] = weapons[i].name;
        }

        // Đảm bảo tất cả vũ khí bị ẩn, chỉ hiện vũ khí đầu tiên khi khởi động
        //EquipWeapon(0);  // Trang bị vũ khí đầu tiên ngay khi bắt đầu
    }

    void Update()
    {
        // Kiểm tra phím bấm để thay đổi vũ khí
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Phím 1 để chọn vũ khí đầu tiên
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Phím 2 để chọn vũ khí thứ hai
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Phím 3 để chọn vũ khí thứ ba
        {
            EquipWeapon(2);
        }

        // Bỏ vũ khí khi nhấn G
        if (Input.GetKeyDown(KeyCode.G) && currentWeapon != null)
        {
            DropWeapon();
        }

        // Nhặt vũ khí khi nhấn F và gần vũ khí rơi
        if (Input.GetKeyDown(KeyCode.F) && droppedWeapon != null)
        {
            PickupWeapon();
        }
    }

    // Phương thức để trang bị vũ khí
    void EquipWeapon(int weaponIndex)
    {
        // Nếu chỉ số vũ khí được chọn giống với vũ khí hiện tại, không cần thay đổi
        if (currentWeaponIndex == weaponIndex) return;

        // Ẩn vũ khí hiện tại
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Hiện vũ khí mới
        if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            currentWeapon = weapons[weaponIndex];
            currentWeapon.SetActive(true);
            currentWeapon.transform.SetParent(weaponHolder); // Đảm bảo vũ khí mới được gắn vào weaponHolder
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeaponIndex = weaponIndex;

            // Cập nhật tên vũ khí trên UI
            UpdateWeaponNameUI();
        }
    }

    // Phương thức để cập nhật tên vũ khí trên giao diện UI
    void UpdateWeaponNameUI()
    {
        if (weaponNameText != null && currentWeaponIndex >= 0 && currentWeaponIndex < weaponNames.Length)
        {
            weaponNameText.text = weaponNames[currentWeaponIndex];
        }
        else
        {
            weaponNameText.text = "No Weapon";
        }
    }

    // Phương thức bỏ vũ khí ra
    void DropWeapon()
    {
        if (currentWeapon != null)
        {
            // Tạo một bản sao của vũ khí đã trang bị để rơi ra đất
            droppedWeapon = Instantiate(currentWeapon, transform.position + transform.forward * 2, Quaternion.identity);
            droppedWeapon.SetActive(true);

            // Tắt vũ khí trên tay player
            currentWeapon.SetActive(false);
            currentWeapon = null;
            currentWeaponIndex = -1;

            // Cập nhật tên vũ khí trên UI
            UpdateWeaponNameUI();
        }
    }

    // Phương thức nhặt vũ khí
    void PickupWeapon()
    {
        if (droppedWeapon != null)
        {
            // Nếu đã có vũ khí trong tay, bỏ vũ khí hiện tại xuống trước khi nhặt vũ khí mới
            if (currentWeapon != null)
            {
                DropWeapon();  // Bỏ vũ khí hiện tại xuống đất
            }

            // Nhặt vũ khí mới
            currentWeapon = droppedWeapon;
            currentWeapon.SetActive(true);

            // Di chuyển vũ khí vào tay player
            currentWeapon.transform.position = weaponHolder.position;
            currentWeapon.transform.rotation = weaponHolder.rotation;
            currentWeapon.transform.SetParent(weaponHolder);

            // Cập nhật lại vũ khí đang trang bị
            droppedWeapon = null;
            // Đặt currentWeaponIndex thành -1 để không liên quan đến danh sách vũ khí mặc định
            currentWeaponIndex = -1;

            // Cập nhật tên vũ khí trên UI
            UpdateWeaponNameUI();

            // Thêm vũ khí vào danh sách vũ khí đang sở hữu
            AddWeaponToInventory(currentWeapon);
        }
    }

    // Thêm vũ khí vào danh sách vũ khí của player (nếu cần)
    void AddWeaponToInventory(GameObject weapon)
    {
        // Thêm logic vào đây để quản lý các vũ khí trong danh sách của player
        // Ví dụ, bạn có thể bổ sung vũ khí vào mảng hoặc danh sách tùy thuộc vào thiết kế của bạn
    }
}
