//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class AmmoBoxInteraction : InteractObject
//{
//    public int ammoSupply = 50; // Số lượng đạn thêm vào khi tương tác
//    public Weapon weapon; // Tham chiếu tới Text Object để hiển thị thông báo

//    protected override void Awake()
//    {
//        base.Awake();
        
//    }

//    private void Start()
//    {
//        //if (ammoText != null)
//        //{
//        //    ammoText.text = "";
//        //    Debug.Log("PickupText initialized and set to empty.");
//        //}
//        //else
//        //{
//        //    Debug.LogError("PickupText is not assigned in the Inspector!");
//        //}
//    }

//    void Update()
//    {

//        if (isPlayerInRange)
//        {
//            Debug.Log("Player is in range.");
//        }

//        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
//        {
//            Debug.Log("F key pressed and player is in range. isPlayerInRange: " + isPlayerInRange);
//            AmmoDisplay player = GameObject.FindWithTag("Player").GetComponent<AmmoDisplay>();
//            if (player != null)
//            {
//                player.AddAmmo(ammoSupply); // Gọi hàm thêm đạn trong script của player
//                Debug.Log("Ammo supplied!");
//                Destroy(gameObject); // Xóa hộp tiếp đạn sau khi sử dụng
//                //if (ammoText != null)
//                //{
//                //    ammoText.text = ""; // Xóa thông báo sau khi sử dụng hộp tiếp đạn
//                //    Debug.Log("PickupText cleared after picking up ammo.");
//                //}
//                //else
//                //{
//                //    Debug.LogError("ammoText is null!");
//                //}
//            }
//            else
//            {
//                Debug.LogError("AmmoDisplay component not found on Player object!");
//            }
//        }
//    }
//}
