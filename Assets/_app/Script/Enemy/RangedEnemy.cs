using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : EnemyAI
{
    public GameObject projectilePrefab; // Tham chiếu đến prefab đạn
    public Transform firePoint; // Tham chiếu đến GameObject khác đại diện cho vị trí bắn

    public override void Attack()
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

    public override void EnemyAction()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position); // Tính khoảng cách đến người chơi

            // Nếu người chơi ở rất gần (0 - minSafeDistance), quái tấn công liên tục
            if (distance < attackRange)
            {
                navAgent.isStopped = true; // Dừng di chuyển để tập trung tấn công

                // Đảm bảo không tấn công liên tục nếu chưa đủ thời gian hồi chiêu
                if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown())
                {
                    transform.LookAt(target);
                    animationCreep.ChangeAnimation("Shoot");
                    Attack(); // Tấn công
                    lastAttackTime = Time.time; // Cập nhật thời gian tấn công
                }
            }
            else
            {
                navAgent.isStopped = false; // Tiếp tục di chuyển nếu không tấn công
                navAgent.SetDestination(target.position); // Quái di chuyển về phía người chơi
                animationCreep.ChangeAnimation("Walk");
            }
        }
    }
}