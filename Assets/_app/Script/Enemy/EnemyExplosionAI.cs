using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Thêm thư viện NavMesh để sử dụng NavMeshAgent

public class EnemyExplosionAI : EnemyAI

{
    // Thêm prefab nổ
    public GameObject explosionPrefab; // Hiệu ứng nổ (Hệ thống hạt hoặc mô hình)
    const int DIE = 99999;

    // Hàm nổ khi quái gần Player
    public override void Attack()
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
        enemyStatus.TakeDamage(DIE);
    }

    public override void EnemyAction()
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
                Attack(); // Thực hiện tấn công
                lastAttackTime = Time.time; // Cập nhật thời gian tấn công lần này
            }
        }
    }
}
