using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Thêm thư viện NavMesh để sử dụng NavMeshAgent

public class EnemyExplosionAI : MonoBehaviour
{
    private float lastAttackTime = 0f; // Thời gian tấn công lần cuối

    public Transform target; // Đối tượng Player (quái sẽ di chuyển về Player)
    private NavMeshAgent navAgent; // Để quái di chuyển theo NavMesh
    private Rigidbody rb; // Rigidbody để xử lý va chạm
    private Enemy enemyStatus;

    // Thêm prefab nổ
    public GameObject explosionPrefab; // Hiệu ứng nổ (Hệ thống hạt hoặc mô hình)

    // Hàm gọi khi game bắt đầu
    void Start()
    {
        enemyStatus = gameObject.GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>(); // Lấy NavMeshAgent từ quái
        rb = GetComponent<Rigidbody>(); // Lấy Rigidbody từ quái

        navAgent.speed = enemyStatus.GetSpeed();
        target = GameObject.FindWithTag("Player").transform;
        navAgent.stoppingDistance = enemyStatus.GetAttackRange(); // Đặt khoảng cách dừng khi quái đến gần Player
    }

    // Hàm cập nhật mỗi khung hình
    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position); // Tính khoảng cách giữa quái và Player

            // Nếu quái còn cách Player quá xa thì tiếp tục di chuyển về phía Player
            if (distance > enemyStatus.GetAttackRange())
            {
                navAgent.SetDestination(target.position); // Di chuyển đến vị trí của Player
            }
            else if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown()) // Nếu quái đủ thời gian giữa các đợt tấn công
            {
                Explode(); // Thực hiện nổ
                lastAttackTime = Time.time; // Cập nhật thời gian tấn công lần này
            }
        }
    }

    // Hàm nổ khi quái gần Player
    void Explode()
    {
        Debug.Log("Quái nổ!");

        // Instantiate hiệu ứng nổ
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Gây sát thương cho player
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>(); // Lấy script PlayerHealth của Player
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(enemyStatus.GetDamage()); // Tấn công Player và giảm máu
        }

        // Hủy quái sau khi nổ
        Destroy(gameObject); // Xóa quái sau khi nổ
    }

    // Hàm xử lý va chạm để không cho quái đẩy Player
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Nếu có va chạm với Player
        {
            transform.Translate(Vector3.zero); // Ngừng di chuyển quái khi va chạm với Player
        }
    }
}
