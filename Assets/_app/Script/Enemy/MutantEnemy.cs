using UnityEngine;

public class MutantEnemy : MeleeEnemy
{
    private float chillDistance;

    private const float CHILL = 15f;
    private const int INSTANLYSPEED = 15;

    public override void Start()
    {
        base.Start();
        chillDistance = attackRange + CHILL;
    }

    public override void EnemyAction()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position); // Tính khoảng cách giữa quái và Player
            if (distance > chillDistance)
            {
                RunCondition(enemyStatus.GetSpeed(), "Walk");
            }
            // Nếu quái còn cách Player quá xa thì tiếp tục di chuyển về phía Player
            else if(distance > attackRange)
            {
                RunCondition(enemyStatus.GetSpeed() + INSTANLYSPEED, "Run");
            }
            else if (Time.time - lastAttackTime >= enemyStatus.GetAttackCooldown()) // Nếu quái đủ thời gian giữa các đợt tấn công
            {
                DealDamage();
            }
        }
    }

    public void RunCondition(int speed, string animation)
    {
        navAgent.speed = speed;
        navAgent.SetDestination(target.position); // Di chuyển đến vị trí của Player
        animationCreep.ChangeAnimation(animation);
    }
}
