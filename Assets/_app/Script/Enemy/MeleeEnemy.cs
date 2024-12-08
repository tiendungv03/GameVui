using UnityEngine;
using UnityEngine.AI; // Sử dụng NavMesh để di chuyển quái

public class MeleeEnemy : MonoBehaviour
{
    private float lastAttackTime = 0f; // Thời gian lần cuối quái tấn công
    private NavMeshAgent navAgent; // Thành phần giúp quái di chuyển theo NavMesh
    private Enemy enemyStatus; // Lấy thông tin trạng thái của quái từ lớp Enemy
    private Transform target; // Đối tượng Player mà quái nhắm tới

    void Start()
    {
        // Lấy dữ liệu trạng thái của quái từ script Enemy
        enemyStatus = GetComponent<Enemy>();

        // Khởi tạo NavMeshAgent để điều khiển quái di chuyển
        navAgent = GetComponent<NavMeshAgent>();

        // Tìm đối tượng Player trong game
        target = GameObject.FindWithTag("Player").transform;

        // Cấu hình NavMeshAgent dựa trên trạng thái của quái
        navAgent.speed = enemyStatus.GetSpeed(); // Tốc độ di chuyển
        navAgent.stoppingDistance = enemyStatus.GetAttackRange(); // Phạm vi dừng trước khi tấn công
    }

    void Update()
    {
        if (target != null)
        {
            // Tính khoảng cách từ quái đến Player
            float distance = Vector3.Distance(target.position, transform.position);

            // Nếu khoảng cách lớn hơn phạm vi tấn công, quái tiếp tục di chuyển
            if (distance > enemyStatus.GetAttackRange())
            {
                navAgent.SetDestination(target.position); // Điều hướng quái đến vị trí Player
            }
            // Nếu khoảng cách trong phạm vi tấn công và quái đã sẵn sàng tấn công
            else if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown())
            {
                Attack(); // Thực hiện hành động tấn công
                lastAttackTime = Time.time; // Cập nhật thời gian lần tấn công cuối
            }
        }
    }

    // Hàm xử lý tấn công
    void Attack()
    {
        Debug.Log("Quái cận chiến tấn công!");
        // Gây sát thương trực tiếp lên Player nếu Player có thành phần PlayerHealth
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(enemyStatus.GetDamage()); // Gây sát thương bằng lượng damage của quái
        }
    }
}
