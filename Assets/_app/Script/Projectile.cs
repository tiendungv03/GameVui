using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;   // Tốc độ của đạn
    public int damage = 10;     // Sát thương của đạn
    private Vector3 direction; // Hướng di chuyển của đạn

    public void SetDirection(Vector3 targetDirection)
    {
        direction = targetDirection.normalized; // Đặt hướng di chuyển, đảm bảo chuẩn hóa
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // Di chuyển đạn theo hướng
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Gây sát thương cho người chơi
            }
            Destroy(gameObject); // Hủy viên đạn sau khi va chạm
        }
    }
}
