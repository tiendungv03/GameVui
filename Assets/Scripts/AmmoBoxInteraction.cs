using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBoxInteraction : MonoBehaviour
{
    public int ammoSupply = 50; // Số lượng đạn thêm vào khi tương tác
    public TextMeshProUGUI ammoText; // Tham chiếu tới Text Object để hiển thị thông báo

    private bool isPlayerInRange = false; // Kiểm tra xem người chơi có trong phạm vi va chạm không

    private void Start()
    {
        if (ammoText != null)
        {
            ammoText.text = "";
            Debug.Log("PickupText initialized and set to empty.");
        }
        else
        {
            Debug.LogError("PickupText is not assigned in the Inspector!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter called with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered the collision zone. isPlayerInRange: " + isPlayerInRange);
            if (ammoText != null)
            {
                ammoText.text = "F: thêm 50 đạn";
                Debug.Log("PickupText updated to show prompt.");
            }
            else
            {
                Debug.LogError("ammoText is null!");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit called with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited the collision zone. isPlayerInRange: " + isPlayerInRange);
            if (ammoText != null)
            {
                ammoText.text = ""; // Xóa thông báo khi người chơi rời khỏi phạm vi
                Debug.Log("PickupText cleared.");
            }
            else
            {
                Debug.LogError("ammoText is null!");
            }
        }
    }

    void Update()
    {

        if (isPlayerInRange)
        {
            Debug.Log("Player is in range.");
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed and player is in range. isPlayerInRange: " + isPlayerInRange);
            AmmoDisplay player = GameObject.FindWithTag("Player").GetComponent<AmmoDisplay>();
            if (player != null)
            {
                player.AddAmmo(ammoSupply); // Gọi hàm thêm đạn trong script của player
                Debug.Log("Ammo supplied!");
                Destroy(gameObject); // Xóa hộp tiếp đạn sau khi sử dụng
                if (ammoText != null)
                {
                    ammoText.text = ""; // Xóa thông báo sau khi sử dụng hộp tiếp đạn
                    Debug.Log("PickupText cleared after picking up ammo.");
                }
                else
                {
                    Debug.LogError("ammoText is null!");
                }
            }
            else
            {
                Debug.LogError("AmmoDisplay component not found on Player object!");
            }
        }
    }
}
