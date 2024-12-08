using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    private float lastAttackTime = 0f;

    public Transform target;
    private NavMeshAgent navAgent;
    private Enemy enemyStatus;

    public GameObject projectilePrefab; // Tham chiếu đến prefab đạn
    public Transform firePoint; // Tham chiếu đến GameObject khác đại diện cho vị trí bắn

    public float minAttackDistance = 6f; // Khoảng cách tối thiểu để tấn công
    public float maxAttackDistance = 8f; // Khoảng cách tối đa để tấn công
    public float minSafeDistance = 4f; // Khoảng cách tối thiểu an toàn, nếu dưới thì quái sẽ lùi

    void Start()
    {
        enemyStatus = GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = enemyStatus.GetSpeed();
        target = GameObject.FindWithTag("Player").transform;
        navAgent.stoppingDistance = enemyStatus.GetAttackRange();
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position); // Tính khoảng cách đến người chơi

            // Nếu người chơi quá gần (< 4m), quái sẽ thử lùi
            if (distance < minSafeDistance)
            {
                MoveAwayFromPlayer(); // Quái lùi nếu không có vật cản phía sau
            }
            // Nếu quái nằm trong khoảng cách tấn công và có thể nhìn thấy người chơi
            else if (distance >= minAttackDistance && distance <= maxAttackDistance && CanSeePlayer())
            {
                navAgent.isStopped = true; // Dừng lại để tấn công

                // Đảm bảo không tấn công liên tục
                if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown()) // Đủ thời gian hồi chiêu
                {
                    RangedAttack(); // Thực hiện tấn công
                    lastAttackTime = Time.time; // Cập nhật thời gian tấn công
                }
            }
            else
            {
                navAgent.isStopped = false; // Tiếp tục di chuyển nếu không tấn công
                navAgent.SetDestination(target.position); // Quái di chuyển về phía người chơi
            }
        }
    }

    void RangedAttack()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Đảm bảo firePoint luôn ở vị trí quái (có thể điều chỉnh nếu cần)
            firePoint.position = transform.position + transform.forward * 0.5f + Vector3.up * 1f; // Đặt firePoint một chút phía trước quái

            // Tạo viên đạn tại vị trí firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Tính hướng từ firePoint tới vị trí người chơi
            Vector3 directionToPlayer = (target.position - firePoint.position).normalized;

            // Truyền hướng và sát thương cho viên đạn
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(directionToPlayer); // Đặt hướng di chuyển của đạn
                projectileScript.damage = enemyStatus.GetDamage(); // Gán sát thương
            }
        }
    }


    void MoveAwayFromPlayer()
    {
        Vector3 directionAway = (transform.position - target.position).normalized; // Hướng ngược lại từ người chơi
        Vector3 newDestination = transform.position + directionAway * 2f; // Di chuyển lùi ra 2m

        // Kiểm tra xem phía sau quái có vật cản không
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionAway, out hit, 2f))
        {
            // Nếu có vật cản phía sau, không lùi mà chuyển sang tấn công
            if (hit.collider != null)
            {
                Debug.Log("Vật cản phía sau, không thể lùi, chuyển sang tấn công.");
                navAgent.isStopped = true;

                // Kiểm tra thời gian hồi chiêu trước khi tấn công
                if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown())
                {
                    // Thực hiện tấn công
                    RangedAttack();
                    lastAttackTime = Time.time; // Cập nhật thời gian tấn công
                }
                else
                {
                    Debug.Log("Không đủ thời gian hồi chiêu để tấn công.");
                }
            }
        }
        else
        {
            // Nếu không có vật cản, quái sẽ lùi ra
            navAgent.isStopped = false;
            navAgent.SetDestination(newDestination);
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (target.position - transform.position).normalized; // Hướng tới người chơi
        float distanceToPlayer = Vector3.Distance(transform.position, target.position); // Khoảng cách tới người chơi

        Ray ray = new Ray(transform.position + Vector3.up * 1f, directionToPlayer); // Tạo ray từ vị trí của quái
        RaycastHit hit;

        // Raycast kiểm tra va chạm
        if (Physics.Raycast(ray, out hit, distanceToPlayer))
        {
            // Nếu raycast trúng người chơi
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

}