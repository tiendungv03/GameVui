using UnityEngine;
using UnityEngine.AI; // Sử dụng NavMesh để di chuyển quái

public class MeleeEnemy : EnemyAI
{
    private float NEARPLAYERMORE = 1.5f;

    public override void Start()
    {
        base.Start();
        navAgent.stoppingDistance = attackRange - NEARPLAYERMORE;
    }

    public override void Attack()
    {
        Debug.Log("Quái cận chiến tấn công!");
        // Gây sát thương trực tiếp lên Player nếu Player có thành phần PlayerHealth
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.Log(playerHealth);
            return;
        }

        playerHealth.TakeDamage(enemyStatus.GetDamage()); // Gây sát thương bằng lượng damage của quái
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
                animationCreep.ChangeAnimation("Walk");
            }
            else if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown()) // Nếu quái đủ thời gian giữa các đợt tấn công
            {
                transform.LookAt(target);
                animationCreep.ChangeAnimation("Punch");
                Attack(); // Thực hiện tấn công
                lastAttackTime = Time.time; // Cập nhật thời gian tấn công lần này
            }
        }
    }

    public void DealDamage()
    {
        transform.LookAt(target);
        animationCreep.ChangeAnimation("Punch");
        Attack(); // Thực hiện tấn công
        lastAttackTime = Time.time; // Cập nhật thời gian tấn công lần này
    }
}
