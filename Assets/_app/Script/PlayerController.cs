using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển
    private Rigidbody rb; // Biến để tham chiếu Rigidbody

    void Start()
    {
        // Lấy Rigidbody từ đối tượng
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Không tìm thấy Rigidbody! Hãy đảm bảo đối tượng có thành phần Rigidbody.");
        }
    }

    void FixedUpdate()
    {
        // Nhận giá trị từ bàn phím
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Tính toán hướng di chuyển
        Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.fixedDeltaTime;

        // Di chuyển nhân vật bằng Rigidbody
        transform.Translate(movement);
    }
}
